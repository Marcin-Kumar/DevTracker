using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers.v1;

/// <summary>
/// API controller for managing goals.
/// </summary>
/// <remarks>
/// Provides endpoints to create, retrieve, update, and delete developer goals.
/// </remarks>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;
    private readonly GoalDtoMapper _goalMapper;

    public GoalsController(IGoalService goalService, GoalDtoMapper goalMapper)
    {
        _goalService = goalService;
        _goalMapper = goalMapper;
    }

    /// <summary>
    /// Creates a new goal.
    /// </summary>
    /// <param name="goal">The goal to create.</param>
    /// <returns>The created goal summary.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GetGoalSummaryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto goal)
    {
        GoalEntity goalEntity = _goalMapper.ToEntity(goal);
        goalEntity = await _goalService.CreateGoal(goalEntity);
        GetGoalSummaryDto goalResponseDto = _goalMapper.ToGetGoalSummaryDto(goalEntity);
        return CreatedAtAction(nameof(ReadGoalById), new { id = goalResponseDto.Id }, goalResponseDto);
    }

    /// <summary>
    /// Retrieves a summary list of all goals.
    /// </summary>
    /// <returns>A list of goal summaries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<GetGoalSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReadAllGoalsAsSummary()
    {
        List<GoalEntity> goalEntities = await _goalService.ReadAllGoals();
        return Ok(goalEntities.ConvertAll(_goalMapper.ToGetGoalSummaryDto));
    }

    /// <summary>
    /// Retrieves a specific goal by ID.
    /// </summary>
    /// <param name="id">The ID of the goal to retrieve.</param>
    /// <returns>The full goal details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetGoalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReadGoalById(int id)
    {
        GoalEntity goalEntity = await _goalService.ReadGoalById(id);
        return Ok(_goalMapper.ToGetGoalDto(goalEntity));
    }

    /// <summary>
    /// Updates an existing goal.
    /// </summary>
    /// <param name="id">The ID of the goal to update.</param>
    /// <param name="goal">The updated goal data.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] UpdateGoalDto goal)
    {
        GoalEntity goalEntity = await _goalService.ReadGoalById(id);
        goalEntity = _goalMapper.ToEntity(goalEntity, goal);
        await _goalService.UpdateGoal(goalEntity);
        return NoContent();
    }

    /// <summary>
    /// Deletes a goal by ID.
    /// </summary>
    /// <param name="id">The ID of the goal to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        await _goalService.DeleteGoal(id);
        return NoContent();
    }
}