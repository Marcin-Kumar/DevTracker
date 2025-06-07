namespace DevTracker.API.Controllers.v1;

using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// API controller for managing coding projects.
/// </summary>
/// <remarks>
/// Provides endpoints to create, retrieve, update, and delete developer projects.
/// Projects can optionally be linked to a goal.
/// </remarks>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ProjectDtoMapper _projectSummaryMapper;

    public ProjectsController(IProjectService projectService, ProjectDtoMapper projectSummaryMapper)
    {
        _projectService = projectService;
        _projectSummaryMapper = projectSummaryMapper;
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="project">The project to create.</param>
    /// <returns>The created project summary.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GetProjectSummaryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves summaries of all projects.
    /// </summary>
    /// <returns>A list of project summaries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetProjectSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReadAllProjectSummaries()
    {
        List<ProjectEntity> projects = await _projectService.ReadAllProjects();
        return Ok(projects.ConvertAll(_projectSummaryMapper.ToGetProjectSummaryDto));
    }

    /// <summary>
    /// Retrieves a specific project by ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <returns>The full project details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReadProjectById(int id)
    {
        ProjectEntity project = await _projectService.ReadProjectById(id);
        return Ok(_projectSummaryMapper.ToGetProjectDto(project));
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="project">The updated project data.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto project)
    {
        ProjectEntity projectEntity = await _projectService.ReadProjectById(id);
        projectEntity = _projectSummaryMapper.ToEntity(projectEntity, project);
        await _projectService.UpdateProject(projectEntity);
        return NoContent();
    }


    /// <summary>
    /// Deletes a project by ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await _projectService.DeleteProject(id);
        return NoContent();
    }
}