namespace DevTracker.API.Controllers;

using DevTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TheorySessionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllTheorySessions()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetTheorySessionById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateTheorySession(TheorySession session)
    {
        return CreatedAtAction(nameof(GetTheorySessionById), new { id = 1 }, session);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTheorySession(int id, TheorySession session)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTheorySession(int id)
    {
        return NoContent();
    }
}
