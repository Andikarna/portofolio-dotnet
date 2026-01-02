using Article = AdidataDbContext.Models.Mysql.PTPDev.PortoArticle;
using AdidataDbContext.Models.Mysql.PTPDev; // Keep this for PTPDevContext if needed, though context is in that namespace.

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PTP.Dto.ArticleDto;
using PTP.Interface;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PTP.Service
{
  public class ArticleService : IArticleService
  {
    private readonly PTPDevContext _context;

    public ArticleService(PTPDevContext context)
    {
      _context = context;
    }

    public async Task<ResponseObject> GetArticlesAsync(int page, int limit, string search)
    {
      try
      {
        var query = _context.Articles.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
          query = query.Where(x => x.Title.Contains(search) || (x.Tags != null && x.Tags.Contains(search)));
        }

        var total = await query.CountAsync();
        var data = await query.OrderByDescending(x => x.PublicationDate)
                              .Skip((page - 1) * limit)
                              .Take(limit)
                              .ToListAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = new
          {
            TotalCount = total,
            Page = page,
            Limit = limit,
            Data = data
          }
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> GetArticleByIdAsync(int id)
    {
      try
      {
        var data = await _context.Articles.FindAsync(id);
        if (data == null)
          return new ResponseObject { Status = 404, Message = "Article not found", Data = null };

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = data
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    private async Task<string> UploadFileAsync(IFormFile file)
    {
      try
      {
        if (file.Length > 0)
        {
          using var ms = new MemoryStream();
          await file.CopyToAsync(ms);
          var fileBytes = ms.ToArray();
          string base64String = Convert.ToBase64String(fileBytes);
          return $"data:{file.ContentType};base64,{base64String}";
        }
        return null;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Base64 Conversion Error: {ex.Message}");
        throw;
      }
    }

    public async Task<ResponseObject> CreateArticleAsync(CreateArticleDto dto)
    {
      try
      {
        var imageBase64 = await UploadFileAsync(dto.ImageFile);

        var article = new Article
        {
          Title = dto.Title,
          Content = dto.Content,
          ImageBase64 = imageBase64,
          PublicationDate = dto.PublicationDate,
          Tags = dto.Tags,
          CreatedDate = DateTime.UtcNow.AddHours(7)
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Article created successfully",
          Data = article
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> UpdateArticleAsync(int id, UpdateArticleDto dto)
    {
      try
      {
        var existing = await _context.Articles.FindAsync(id);
        if (existing == null)
          return new ResponseObject { Status = 404, Message = "Article not found", Data = null };

        existing.ImageBase64 = await UploadFileAsync(dto.ImageFile);

        existing.Title = dto.Title;
        existing.Content = dto.Content;
        existing.PublicationDate = dto.PublicationDate;
        existing.Tags = dto.Tags;
        existing.UpdateDate = DateTime.UtcNow.AddHours(7);

        _context.Articles.Update(existing);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Article updated successfully",
          Data = existing
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> DeleteArticleAsync(int id)
    {
      try
      {
        var existing = await _context.Articles.FindAsync(id);
        if (existing == null)
          return new ResponseObject { Status = 404, Message = "Article not found", Data = null };

        _context.Articles.Remove(existing);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Article deleted successfully",
          Data = null
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }
  }
}
