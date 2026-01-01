using System.ComponentModel.DataAnnotations;

namespace PTP.Dto.SkillDto
{
  public class CreateSkillDto
  {
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Category { get; set; } = null!;
    [Required]
    public string Level { get; set; } = null!;

    public string? IconUrl { get; set; }

    public bool IsFeatured { get; set; }
  }
}
