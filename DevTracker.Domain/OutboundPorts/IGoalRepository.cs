using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IGoalRepository
{
    public abstract void CreateGoal(GoalEntity goal);
    public abstract void UpdateGoal(GoalEntity goal);
    public abstract void DeleteGoal(int id);
    public abstract void ReadAllGoals();
}
