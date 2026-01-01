using System.Threading.Tasks;
using AdidataDbContext.Models.Mysql.PTPDev;
using PTP.Dto.SkillDto;
using PTP.Models;

namespace PTP.Interface
{
  public interface ISkillService
  {
    Task<ResponseObject> GetSkillsAsync(int page, int limit, string? search = null);
    Task<ResponseObject> GetSkillByIdAsync(int id);
    Task<ResponseObject> CreateSkillAsync(CreateSkillDto dto);
    Task<ResponseObject> UpdateSkillAsync(int id, UpdateSkillDto dto);
    Task<ResponseObject> DeleteSkillAsync(int id);
  }
}
