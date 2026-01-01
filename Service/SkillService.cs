using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdidataDbContext.Models.Mysql.PTPDev;
using PTP.Dto.SkillDto;
using PTP.Interface;
using PTP.Models;

namespace PTP.Service
{
  public class SkillService : ISkillService
  {
    private readonly PTPDevContext _context;

    public SkillService(PTPDevContext context)
    {
      _context = context;
    }

    public async Task<ResponseObject> GetSkillsAsync(int page, int limit, string? search = null)
    {
      try
      {
        var query = _context.Skills.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
          query = query.Where(s => s.Name.Contains(search) || s.Category.Contains(search));
        }

        var total = await query.CountAsync();
        var data = await query
            .OrderByDescending(s => s.CreatedDate)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var pagination = new
        {
          Total = total,
          Page = page,
          Limit = limit,
          TotalPages = (int)Math.Ceiling(total / (double)limit)
        };

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = new { Items = data, Pagination = pagination }
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message
        };
      }
    }

    public async Task<ResponseObject> GetSkillByIdAsync(int id)
    {
      try
      {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
          return new ResponseObject
          {
            Status = 404,
            Message = "Skill not found"
          };
        }

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = skill
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message
        };
      }
    }

    public async Task<ResponseObject> CreateSkillAsync(CreateSkillDto dto)
    {
      try
      {
        var skill = new Skill
        {
          Name = dto.Name,
          Category = dto.Category,
          Level = dto.Level,
          IconUrl = dto.IconUrl,
          IsFeatured = dto.IsFeatured,
          CreatedDate = DateTime.Now,
          UpdateDate = DateTime.Now
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Skill created successfully",
          Data = skill
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message
        };
      }
    }

    public async Task<ResponseObject> UpdateSkillAsync(int id, UpdateSkillDto dto)
    {
      try
      {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
          return new ResponseObject
          {
            Status = 404,
            Message = "Skill not found"
          };
        }

        if (dto.Name != null) skill.Name = dto.Name;
        if (dto.Category != null) skill.Category = dto.Category;
        if (dto.Level != null) skill.Level = dto.Level;
        if (dto.IconUrl != null) skill.IconUrl = dto.IconUrl;
        if (dto.IsFeatured.HasValue) skill.IsFeatured = dto.IsFeatured.Value;

        skill.UpdateDate = DateTime.Now;

        _context.Skills.Update(skill);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Skill updated successfully",
          Data = skill
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message
        };
      }
    }

    public async Task<ResponseObject> DeleteSkillAsync(int id)
    {
      try
      {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
        {
          return new ResponseObject
          {
            Status = 404,
            Message = "Skill not found"
          };
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Skill deleted successfully"
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message
        };
      }
    }
  }
}
