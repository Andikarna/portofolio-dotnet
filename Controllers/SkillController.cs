using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTP.Components;
using PTP.Dto.SkillDto;
using PTP.Interface;
using System.Threading.Tasks;

namespace PTP.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class SkillController : ControllerBase
  {
    private readonly ISkillService _skillService;
    private readonly ResponseStatusHeader _header;

    public SkillController(ISkillService skillService, ResponseStatusHeader header)
    {
      _skillService = skillService;
      _header = header;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int limit = 10, [FromQuery] string? search = null)
    {
      var result = await _skillService.GetSkillsAsync(page, limit, search);
      return _header.BuildResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
      var result = await _skillService.GetSkillByIdAsync(id);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSkillDto dto)
    {
      var result = await _skillService.CreateSkillAsync(dto);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateSkillDto dto)
    {
      var result = await _skillService.UpdateSkillAsync(id, dto);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
      var result = await _skillService.DeleteSkillAsync(id);
      return _header.BuildResponse(result);
    }
  }
}
