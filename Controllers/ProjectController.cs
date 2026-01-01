using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTP.Components;
using PTP.Dto.ProjectDto;
using PTP.Interface;
using System.Threading.Tasks;

namespace PTP.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    private readonly IProjectService _projectService;
    private readonly ResponseStatusHeader _header;

    public ProjectController(IProjectService projectService, ResponseStatusHeader header)
    {
      _projectService = projectService;
      _header = header;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int limit = 10, [FromQuery] string? search = null)
    {
      var result = await _projectService.GetProjectsAsync(page, limit, search);
      return _header.BuildResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
      var result = await _projectService.GetProjectByIdAsync(id);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProjectDto dto)
    {
      var result = await _projectService.CreateProjectAsync(dto);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] int id, [FromForm] UpdateProjectDto dto)
    {
      var result = await _projectService.UpdateProjectAsync(id, dto);
      return _header.BuildResponse(result);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
      var result = await _projectService.DeleteProjectAsync(id);
      return _header.BuildResponse(result);
    }
  }
}
