using DevTracker.Data.Context;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Ports;

namespace DevTracker.Data.Adapters;

public class GoalRepository : IGoalRepository
{
    private readonly DevTrackerContext _context;
    public GoalRepository(DevTrackerContext context)
    {
        _context = context;
    }
    public void CreateGoal(GoalEntity goal)
    {
        throw new NotImplementedException();
    }

    public void UpdateGoal(GoalEntity goal)
    {
        throw new NotImplementedException();
    }

    public void DeleteGoal(int id)
    {
        throw new NotImplementedException();
    }

    public void ReadAllGoals()
    {
        throw new NotImplementedException();
    }
}