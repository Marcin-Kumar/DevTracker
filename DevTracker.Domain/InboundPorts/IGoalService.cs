using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
public interface IGoalService
{
    public abstract List<Goal> GetAllGoals();
    public abstract Goal GetGoalById();
    public abstract void CreateGoal();
    public abstract void DeleteGoal();
    public abstract void UpdateGoal();
}
