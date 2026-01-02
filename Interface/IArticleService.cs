using PTP.Dto.ArticleDto;
using PTP.Service; // For ResponseObject
using System.Threading.Tasks;

namespace PTP.Interface
{
  public interface IArticleService
  {
    Task<ResponseObject> GetArticlesAsync(int page, int limit, string search);
    Task<ResponseObject> GetArticleByIdAsync(int id);
    Task<ResponseObject> CreateArticleAsync(CreateArticleDto dto);
    Task<ResponseObject> UpdateArticleAsync(int id, UpdateArticleDto dto);
    Task<ResponseObject> DeleteArticleAsync(int id);
  }
}
