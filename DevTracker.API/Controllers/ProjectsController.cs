namespace DevTracker.API.Controllers;

using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.InboundPorts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ProjectDtoMapper _projectSummaryMapper;

    public ProjectsController(IProjectService projectService, ProjectDtoMapper projectSummaryMapper)
    {
        _projectService = projectService;
        _projectSummaryMapper = projectSummaryMapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto project)
    {
        ProjectEntity projectEntity = _projectSummaryMapper.ToEntity(project);

        if (project.GoalId is not null)
        {
            projectEntity = await _projectService.CreateProjectForGoalWithId((int)project.GoalId, projectEntity);
        }
        else
        {
            projectEntity = await _projectService.CreateProject(projectEntity);
        }
        GetProjectSummaryDto projectResponseDto = _projectSummaryMapper.ToGetProjectSummaryDto(projectEntity);
        return CreatedAtAction(nameof(ReadProjectById), new { id = projectResponseDto.Id }, projectResponseDto);
    }

    [HttpGet]
    public async Task<IActionResult> ReadAllProjectSummaries()
    {
        List<ProjectEntity> projects = await _projectService.ReadAllProjects();
        return Ok(projects.ConvertAll(_projectSummaryMapper.ToGetProjectSummaryDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadProjectById(int id)
    {
        ProjectEntity project = await _projectService.ReadProjectById(id);
        return Ok(_projectSummaryMapper.ToGetProjectDto(project));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto project)
    {
        ProjectEntity projectEntity = await _projectService.ReadProjectById(id);
        projectEntity = _projectSummaryMapper.ToEntity(projectEntity, project);
        await _projectService.UpdateProject(projectEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await _projectService.DeleteProject(id);
        return NoContent();
    }
}