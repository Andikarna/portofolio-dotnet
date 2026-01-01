using System.Threading.Tasks;
using AdidataDbContext.Models.Mysql.PTPDev;
using PTP.Dto.ProjectDto;
using PTP.Models;

namespace PTP.Interface
{
  public interface IProjectService
  {
    Task<ResponseObject> GetProjectsAsync(int page, int limit, string? search = null);
    Task<ResponseObject> GetProjectByIdAsync(int id);
    Task<ResponseObject> CreateProjectAsync(CreateProjectDto dto);
    Task<ResponseObject> UpdateProjectAsync(int id, UpdateProjectDto dto);
    Task<ResponseObject> DeleteProjectAsync(int id);
  }
}
