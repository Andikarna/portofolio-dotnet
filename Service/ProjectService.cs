using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AdidataDbContext.Models.Mysql.PTPDev;
using PTP.Dto.ProjectDto;
using PTP.Interface;
using PTP.Models;

namespace PTP.Service
{
  public class ProjectService : IProjectService
  {
    private readonly PTPDevContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProjectService(PTPDevContext context, IWebHostEnvironment environment)
    {
      _context = context;
      _environment = environment;
    }

    private async Task<string> UploadFileAsync(IFormFile file)
    {
      try
      {
        if (file.Length > 0)
        {
          using var ms = new MemoryStream();
          await file.CopyToAsync(ms);
          var fileBytes = ms.ToArray();
          string base64String = Convert.ToBase64String(fileBytes);
          return $"data:{file.ContentType};base64,{base64String}";
        }
        return null;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Base64 Conversion Error: {ex.Message}");
        throw;
      }
    }

    public async Task<ResponseObject> GetProjectsAsync(int page, int limit, string? search = null)
    {
      try
      {
        var query = _context.Projects.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
          query = query.Where(p => p.Title.Contains(search) || (p.Technologies != null && p.Technologies.Contains(search)));
        }

        var total = await query.CountAsync();
        var data = await query
            .OrderByDescending(p => p.CreatedDate)
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

    public async Task<ResponseObject> GetProjectByIdAsync(int id)
    {
      try
      {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
          return new ResponseObject
          {
            Status = 404,
            Message = "Project not found"
          };
        }

        return new ResponseObject
        {
          Status = 200,
          Message = "Success",
          Data = project
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

    public async Task<ResponseObject> CreateProjectAsync(CreateProjectDto dto)
    {
      try
      {
        string? coverImageUrl = dto.CoverImageUrl;
        if (dto.ImageFile != null)
        {
          coverImageUrl = await UploadFileAsync(dto.ImageFile);
        }

        var project = new Project
        {
          Title = dto.Title,
          CoverImageUrl = coverImageUrl,
          RepositoryUrl = dto.RepositoryUrl,
          DemoUrl = dto.DemoUrl,
          Status = dto.Status,
          StartDate = dto.StartDate,
          Technologies = dto.Technologies,
          Summary = dto.Summary,
          Description = dto.Description,
          IsFeatured = dto.IsFeatured,
          CreatedDate = DateTime.Now,
          UpdateDate = DateTime.Now
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Project created successfully",
          Data = project
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

    public async Task<ResponseObject> UpdateProjectAsync(int id, UpdateProjectDto dto)
    {
      try
      {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
          return new ResponseObject
          {
            Status = 404,
            Message = "Project not found"
          };
        }

        if (dto.ImageFile != null)
        {
          project.CoverImageUrl = await UploadFileAsync(dto.ImageFile);
        }
        else if (dto.CoverImageUrl != null)
        {
          project.CoverImageUrl = dto.CoverImageUrl;
        }

        if (dto.Title != null) project.Title = dto.Title;
        if (dto.RepositoryUrl != null) project.RepositoryUrl = dto.RepositoryUrl;
        if (dto.DemoUrl != null) project.DemoUrl = dto.DemoUrl;
        if (dto.Status != null) project.Status = dto.Status;
        if (dto.StartDate.HasValue) project.StartDate = dto.StartDate;
        if (dto.Technologies != null) project.Technologies = dto.Technologies;
        if (dto.Summary != null) project.Summary = dto.Summary;
        if (dto.Description != null) project.Description = dto.Description;
        if (dto.IsFeatured.HasValue) project.IsFeatured = dto.IsFeatured.Value;

        project.UpdateDate = DateTime.Now;

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Project updated successfully",
          Data = project
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

    public async Task<ResponseObject> DeleteProjectAsync(int id)
    {
      try
      {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
          return new ResponseObject
          {
            Status = 404,
            Message = "Project not found"
          };
        }

        // Optional: Delete image file from storage if existing
        // if (!string.IsNullOrEmpty(project.CoverImageUrl)) { ... }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return new ResponseObject
        {
          Status = 200,
          Message = "Project deleted successfully"
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
