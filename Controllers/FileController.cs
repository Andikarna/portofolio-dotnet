using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using PTP.Dto;

namespace PTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMinioClient _minio;
        private readonly MinioSettings _config;

        public FileController(IMinioClient minio, IOptions<MinioSettings> config)
        {
            _minio = minio;
            _config = config.Value;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null) return BadRequest("File tidak ditemukan");

            string bucket = _config.Bucket;

            bool bucketExists = await _minio.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(bucket));

            if (!bucketExists)
                await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket));

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();

            await _minio.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(fileName)
                    .WithContentType(file.ContentType)
                    .WithObjectSize(file.Length)
                    .WithStreamData(stream)
            );

            string fileUrl = $"http://{_config.Endpoint}/{bucket}/{fileName}";

            return Ok(new { message = "success", fileUrl });
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            string bucket = _config.Bucket;

            bool bucketExists = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));
            if (!bucketExists) return Ok(new { files = Array.Empty<string>() });

            var objects = new List<string>();
            var tcs = new TaskCompletionSource<bool>();

            var subscription = _minio.ListObjectsAsync(
                new ListObjectsArgs().WithBucket(bucket).WithRecursive(true))
                .Subscribe(
                    item => objects.Add(item.Key),
                    ex => tcs.SetException(ex),
                    () => tcs.SetResult(true)
                );

            await tcs.Task;  // tunggu sampai observable selesai
            subscription.Dispose();

            return Ok(new { files = objects });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
                return BadRequest(new { message = "Query tidak boleh kosong" });

            string bucket = _config.Bucket;

            bool bucketExists = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));
            if (!bucketExists) return Ok(new { files = Array.Empty<object>() });

            var results = new List<object>();
            var tcs = new TaskCompletionSource<bool>();

            var subscription = _minio.ListObjectsAsync(
                new ListObjectsArgs().WithBucket(bucket).WithRecursive(true))
                .Subscribe(
                    item =>
                    {
                        if (item.Key.Contains(query, StringComparison.OrdinalIgnoreCase))
                        {
                            // Generate URL untuk preview
                            string previewUrl = $"http://{_config.Endpoint}/{bucket}/{item.Key}";
                            results.Add(new { fileName = item.Key, previewUrl });
                        }
                    },
                    ex => tcs.SetException(ex),
                    () => tcs.SetResult(true)
                );

            await tcs.Task;
            subscription.Dispose();

            return Ok(results);
        }




        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            string bucket = _config.Bucket;

            try
            {
                var memoryStream = new MemoryStream();
                await _minio.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                    }));

                memoryStream.Position = 0;
                return File(memoryStream, "application/octet-stream", fileName);
            }
            catch (MinioException e)
            {
                return NotFound(new { message = e.Message });
            }
        }


        // ======================
        // Update File (replace existing)
        // ======================
        [HttpPut("update/{fileName}")]
        public async Task<IActionResult> Update(string fileName, IFormFile file)
        {
            if (file == null) return BadRequest("File tidak ditemukan");

            string bucket = _config.Bucket;

            // Hapus file lama jika ada
            bool exists = false;
            try
            {
                await _minio.StatObjectAsync(new StatObjectArgs().WithBucket(bucket).WithObject(fileName));
                exists = true;
            }
            catch (MinioException)
            {
                exists = false;
            }

            if (exists)
            {
                await _minio.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(bucket).WithObject(fileName));
            }

            // Upload file baru dengan nama sama
            using var stream = file.OpenReadStream();
            await _minio.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(fileName)
                .WithContentType(file.ContentType)
                .WithObjectSize(file.Length)
                .WithStreamData(stream));

            string fileUrl = $"http://{_config.Endpoint}/{bucket}/{fileName}";
            return Ok(new { message = "success", fileUrl });
        }
    }
}
