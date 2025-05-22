using DevTracker.Core.Domain.Entities;
using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Data.Models;
using DevTracker.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Data.Adapters;

public class GoalRepository : IGoalRepository
{
    private readonly DevTrackerContext _context;
    private readonly GoalDataMapper _goalMapper;
    
    public GoalRepository(DevTrackerContext context, GoalDataMapper goalMapper)
    {
        _context = context;
        _goalMapper = goalMapper;
    }
    public async Task<GoalEntity> CreateGoal(GoalEntity goal)
    {
        Goal goalModel = _goalMapper.ToModel(goal);
        await _context.Goals.AddAsync(goalModel);
        await _context.SaveChangesAsync();
        return _goalMapper.ToEntity(goalModel);
    }

    public async Task UpdateGoal(GoalEntity goal)
    {
        Goal goalModel = _goalMapper.ToModel(goal);
        _context.Goals.Update(goalModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGoal(int id)
    {
        _context.Goals.Remove(new Goal { Id = id, Title = "delete"});
        await _context.SaveChangesAsync();
    }

    public async Task<List<GoalEntity>> ReadAllGoals()
    {
        List<Goal> goals = await _context.Goals.AsNoTracking().ToListAsync();
        return goals.ConvertAll(_goalMapper.ToEntity).ToList();
    }
    public async Task<GoalEntity> ReadGoalById(int id)
    {
        Goal? goalModel = await _context.Goals
                                    .Include(g => g.Projects)
                                    .Include(g => g.CodingSessions)
                                    .Include(g => g.TheorySessions)
                                    .Where(g => g.Id == id)
                                    .SingleOrDefaultAsync();
        if (goalModel is null)
        {
            throw new KeyNotFoundException($"Goal with ID {id} not found.");
        }
        return _goalMapper.ToEntity(goalModel);
    }
}