using Microsoft.AspNetCore.Http;
using System;

namespace PTP.Dto.ArticleDto
{
  public class CreateArticleDto
  {
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? ImageFile { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? Tags { get; set; }
  }
}
