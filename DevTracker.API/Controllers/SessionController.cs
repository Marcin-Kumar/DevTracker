namespace DevTracker.API.Controllers;

using DevTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllSessions()
    {
        return Ok(); 
    }

    [HttpGet("by-type")]
    public IActionResult GetSessionsByType([FromQuery] int? type)
    {
        return Ok(); 
    }

    [HttpGet("{id}")]
    public IActionResult GetSessionById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateSession(SessionEntity session)
    {
        return CreatedAtAction(nameof(GetSessionById), new { id = 1 }, session);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSession(int id, SessionEntity session)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSession(int id)
    {
        return NoContent();
    }
}
