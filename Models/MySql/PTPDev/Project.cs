using System;

namespace AdidataDbContext.Models.Mysql.PTPDev
{
  public class Project
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? Status { get; set; } // e.g., On Progress, Completed
    public DateTime? StartDate { get; set; }
    public string? Technologies { get; set; } // Comma separated, e.g. "React, .NET"
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
  }
}
