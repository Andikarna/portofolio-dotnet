using System;
using System.Collections.Generic;

namespace AdidataDbContext.Models.Mysql.PTPDev
{
  public class Experience
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Skills { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
  }
}