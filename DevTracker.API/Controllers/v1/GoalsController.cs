﻿using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;
    private readonly GoalDtoMapper _goalMapper;

    public GoalsController(IGoalService goalService, GoalDtoMapper goalMapper)
    {
        _goalService = goalService;
        _goalMapper = goalMapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto goal)
    {
        GoalEntity goalEntity = _goalMapper.ToEntity(goal);
        goalEntity = await _goalService.CreateGoal(goalEntity);
        GetGoalSummaryDto goalResponseDto = _goalMapper.ToGetGoalSummaryDto(goalEntity);
        return CreatedAtAction(nameof(ReadGoalById), new { id = goalResponseDto.Id }, goalResponseDto);
    }

    [HttpGet]
    public async Task<IActionResult> ReadAllGoalsAsSummary()
    {
        List<GoalEntity> goalEntities = await _goalService.ReadAllGoals();
        return Ok(goalEntities.ConvertAll(_goalMapper.ToGetGoalSummaryDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadGoalById(int id)
    {
        GoalEntity goalEntity = await _goalService.ReadGoalById(id);
        return Ok(_goalMapper.ToGetGoalDto(goalEntity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] UpdateGoalDto goal)
    {
        GoalEntity goalEntity = await _goalService.ReadGoalById(id);
        goalEntity = _goalMapper.ToEntity(goalEntity, goal);
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