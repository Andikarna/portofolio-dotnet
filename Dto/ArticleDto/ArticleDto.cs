using System;

namespace PTP.Dto.ArticleDto
{
  public class ArticleDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? ImageBase64 { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? Tags { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
  }
}
