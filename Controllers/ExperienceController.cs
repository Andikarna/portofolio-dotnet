using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTP.Components;
using PTP.Dto.ExperienceDto;
using PTP.Interface;
using System.Threading.Tasks;

namespace PTP.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ExperienceController : ControllerBase
  {
    private readonly IExperienceService _experienceService;
    private readonly ResponseStatusHeader _header;

    public ExperienceController(IExperienceService experienceService, ResponseStatusHeader header)
    {
      _experienceService = experienceService;
      _header = header;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
      var result = await _experienceService.GetExperiencesAsync(page, limit);
      return _header.BuildResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
      var result = await _experienceService.GetExperienceByIdAsync(id);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExperienceDto dto)
    {
      var result = await _experienceService.CreateExperienceAsync(dto);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateExperienceDto dto)
    {
      var result = await _experienceService.UpdateExperienceAsync(id, dto);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
      var result = await _experienceService.DeleteExperienceAsync(id);
      return _header.BuildResponse(result);
    }
  }
}
