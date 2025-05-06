using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IGoalRepository
{
    public abstract void CreateGoal(Goal goal);
    public abstract void UpdateGoal(Goal goal);
    public abstract void DeleteGoal(Guid id);
    public abstract void ReadAllGoals();
}
