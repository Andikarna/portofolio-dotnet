using System;

namespace PTP.Dto.ExperienceDto
{
  public class CreateExperienceDto
  {
    public string Title { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Skills { get; set; }
  }

  public class UpdateExperienceDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Skills { get; set; }
  }

  public class ExperienceListDto
  {
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; }
    public object Data { get; set; }
  }
}
