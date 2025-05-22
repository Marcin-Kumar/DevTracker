using DevTracker.Core.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IGoalRepository
{
    public abstract Task<GoalEntity> CreateGoal(GoalEntity goal);
    public abstract Task UpdateGoal(GoalEntity goal);
    public abstract Task DeleteGoal(int id);
    public abstract Task<List<GoalEntity>> ReadAllGoals();
    public abstract Task<GoalEntity> ReadGoalById(int id);
}
