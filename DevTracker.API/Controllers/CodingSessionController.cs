namespace DevTracker.API.Controllers;

using DevTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CodingSessionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCodingSessions()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetCodingSessionById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateCodingSession(CodingSession session)
    {
        return CreatedAtAction(nameof(GetCodingSessionById), new { id = 1 }, session);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCodingSession(int id, CodingSession session)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCodingSession(int id)
    {
        return NoContent();
    }
}