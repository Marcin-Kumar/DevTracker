namespace DevTracker.API.Controllers;

using DevTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllProjects()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetProjectById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateProject(Project project)
    {
        return CreatedAtAction(nameof(GetProjectById), new { id = 1 }, project);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, Project project)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        return NoContent();
    }
}