using System;
using Microsoft.AspNetCore.Http;

namespace PTP.Dto.ProjectDto
{
  public class UpdateProjectDto
  {
    public IFormFile? ImageFile { get; set; }
    public string? Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public string? Technologies { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public bool? IsFeatured { get; set; }
  }
}
