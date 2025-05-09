using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllGoals()
    {
        return Ok(new List<string> { "Goal 1", "Goal 2" });
    }

    [HttpGet("{id}")]
    public IActionResult GetGoalById(int id)
    {
        return Ok($"Goal {id}");
    }

    [HttpPost]
    public IActionResult CreateGoal([FromBody] string goal)
    {
        return CreatedAtAction(nameof(GetGoalById), new { id = 1 }, goal);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGoal(int id, [FromBody] string goal)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGoal(int id)
    {
        return NoContent();
    }
}
