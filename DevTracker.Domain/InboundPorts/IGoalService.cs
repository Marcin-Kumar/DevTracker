using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
public interface IGoalService
{
    public abstract List<GoalEntity> GetAllGoals();
    public abstract GoalEntity GetGoalById();
    public abstract void CreateGoal();
    public abstract void DeleteGoal();
    public abstract void UpdateGoal();
}
