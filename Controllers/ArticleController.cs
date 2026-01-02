using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTP.Components;
using PTP.Dto.ArticleDto;
using PTP.Interface;
using System.Threading.Tasks;

namespace PTP.Controllers
{
  [Authorize]
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ArticleController : ControllerBase
  {
    private readonly IArticleService _service;
    private readonly ResponseStatusHeader _header;

    public ArticleController(IArticleService service, ResponseStatusHeader header)
    {
      _service = service;
      _header = header;
    }

    [HttpGet]
    public async Task<IActionResult> List(int page = 1, int limit = 10, string search = "")
    {
      var result = await _service.GetArticlesAsync(page, limit, search);
      return _header.BuildResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detail(int id)
    {
      var result = await _service.GetArticleByIdAsync(id);
      return _header.BuildResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateArticleDto dto)
    {
      var result = await _service.CreateArticleAsync(dto);
      return _header.BuildResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateArticleDto dto)
    {
      var result = await _service.UpdateArticleAsync(id, dto);
      return _header.BuildResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await _service.DeleteArticleAsync(id);
      return _header.BuildResponse(result);
    }
  }
}
