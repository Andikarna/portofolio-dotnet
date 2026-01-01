using System;
using System.Collections.Generic;

namespace AdidataDbContext.Models.Mysql.PTPDev
{
  public class Skill
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string? IconUrl { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
  }
}
