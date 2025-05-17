using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Data.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Data.Adapters;

public class GoalRepository : IGoalRepository
{
    private readonly DevTrackerContext _context;
    private readonly GoalMapper _goalMapper;
    public GoalRepository(DevTrackerContext context, GoalMapper goalMapper)
    {
        _context = context;
        _goalMapper = goalMapper;
    }
    public async Task CreateGoal(GoalEntity goal)
    {
        Goal goalModel = _goalMapper.ToModel(goal);
        await _context.Goals.AddAsync(goalModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGoal(GoalEntity goal)
    {
        Goal goalModel = _goalMapper.ToModel(goal);
        _context.Goals.Update(goalModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGoal(int id)
    {
        _context.Goals.Remove(new Goal { Id = id});
        await _context.SaveChangesAsync();
    }

    public async Task<List<GoalEntity>> ReadAllGoals()
    {
        List<Goal> goals = await _context.Goals.AsNoTracking().ToListAsync();
        return goals.Select(_goalMapper.ToEntity).ToList();
    }
    public async Task<GoalEntity> ReadGoalById(int id)
    {
        Goal goalModel = await _context.Goals
                                    .Include(g => g.Projects)
                                    .Include(g => g.CodingSessions)
                                    .Include(g => g.TheorySessions)
                                    .Where(g => g.Id == id)
                                    .SingleOrDefaultAsync(); 
        return _goalMapper.ToEntity(goalModel); 
    }
}