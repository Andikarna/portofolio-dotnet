using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.EntityFrameworkCore;
using PTP.Dto.ExperienceDto;
using PTP.Interface;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace PTP.Service
{
  public class ExperienceService : IExperienceService
  {
    private readonly PTPDevContext _context;

    public ExperienceService(PTPDevContext context)
    {
      _context = context;
    }

    public async Task<ResponseObject> GetExperiencesAsync(int page, int limit)
    {
      try
      {
        var query = _context.Experiences.AsQueryable();
        var total = await query.CountAsync();
        var data = await query.OrderByDescending(x => x.StartDate)
                              .Skip((page - 1) * limit)
                              .Take(limit)
                              .ToListAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = new
          {
            TotalCount = total,
            Page = page,
            Limit = limit,
            Data = data
          }
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> GetExperienceByIdAsync(int id)
    {
      try
      {
        var data = await _context.Experiences.FindAsync(id);
        if (data == null)
          return new ResponseObject { Status = 404, Message = "Experience not found", Data = null };

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = data
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> CreateExperienceAsync(CreateExperienceDto dto)
    {
      try
      {
        var experience = new Experience
        {
          Title = dto.Title,
          Company = dto.Company,
          Description = dto.Description,
          Status = dto.Status,
          StartDate = dto.StartDate,
          EndDate = dto.EndDate,
          Skills = dto.Skills,
          CreatedDate = DateTime.UtcNow.AddHours(7)
        };

        _context.Experiences.Add(experience);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Experience created successfully",
          Data = experience
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> UpdateExperienceAsync(int id, UpdateExperienceDto dto)
    {
      try
      {
        var existing = await _context.Experiences.FindAsync(id);
        if (existing == null)
          return new ResponseObject { Status = 404, Message = "Experience not found", Data = null };

        existing.Title = dto.Title;
        existing.Company = dto.Company;
        existing.Description = dto.Description;
        existing.Status = dto.Status;
        existing.StartDate = dto.StartDate;
        existing.EndDate = dto.EndDate;
        existing.Skills = dto.Skills;
        existing.UpdateDate = DateTime.UtcNow.AddHours(7);

        _context.Experiences.Update(existing);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Experience updated successfully",
          Data = existing
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }

    public async Task<ResponseObject> DeleteExperienceAsync(int id)
    {
      try
      {
        var existing = await _context.Experiences.FindAsync(id);
        if (existing == null)
          return new ResponseObject { Status = 404, Message = "Experience not found", Data = null };

        _context.Experiences.Remove(existing);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Experience deleted successfully",
          Data = null
        };
      }
      catch (Exception ex)
      {
        return new ResponseObject
        {
          Status = 500,
          Message = "Error: " + ex.Message,
          Data = null
        };
      }
    }
  }
}
