using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.InboundPorts;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;
    private readonly GoalMapper _goalMapper;

    public GoalController(IGoalService goalService, GoalMapper goalMapper)
    {
        _goalService = goalService;
        _goalMapper = goalMapper;
    }

    [HttpGet]
    public async Task<IActionResult> ReadAllGoalsAsSummary()
    {
        List<GoalEntity> goalEntities = await _goalService.ReadAllGoals();
        return Ok(goalEntities.Select(_goalMapper.ToGetGoalSummaryDto).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadGoalById(int id)
    {
        GoalEntity goalEntity = await _goalService.ReadGoalById(id);
        return Ok(_goalMapper.ToGetGoalDto(goalEntity)); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto goal)
    {
        GoalEntity goalEntity = _goalMapper.toEntity(goal);  
        goalEntity = await _goalService.CreateGoal(goalEntity);
        GetGoalSummaryDto goalResponseDto = _goalMapper.ToGetGoalSummaryDto(goalEntity);
        return CreatedAtAction(nameof(ReadGoalById), new { id = goalResponseDto.Id }, goalResponseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] UpdateGoalDto goal)
    {
        GoalEntity goalEntity = await _goalService.ReadGoalById(id);
        goalEntity = _goalMapper.toEntity(goalEntity, goal);
        await _goalService.UpdateGoal(goalEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        await _goalService.DeleteGoal(id);
        return NoContent();
    }
}