using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PTP.Dto.ProjectDto
{
  public class CreateProjectDto
  {
    public IFormFile? ImageFile { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? CoverImageUrl { get; set; }

    public string? RepositoryUrl { get; set; }

    public string? DemoUrl { get; set; }

    public string? Status { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Technologies { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public bool IsFeatured { get; set; }
  }
}
