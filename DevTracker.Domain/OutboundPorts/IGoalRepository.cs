using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IGoalRepository
{
    public abstract void CreateGoal(GoalEntity goal);
    public abstract void UpdateGoal(GoalEntity goal);
    public abstract void DeleteGoal(Guid id);
    public abstract void ReadAllGoals();
}
