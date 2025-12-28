using PTP.Dto.ExperienceDto;

using System.Threading.Tasks;

namespace PTP.Interface
{
  public interface IExperienceService
  {
    Task<ResponseObject> GetExperiencesAsync(int page, int limit);
    Task<ResponseObject> GetExperienceByIdAsync(int id);
    Task<ResponseObject> CreateExperienceAsync(CreateExperienceDto dto);
    Task<ResponseObject> UpdateExperienceAsync(int id, UpdateExperienceDto dto);
    Task<ResponseObject> DeleteExperienceAsync(int id);
  }
}
