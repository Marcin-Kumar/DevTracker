using DevTracker.Core.Domain.Entities;

namespace DevTracker.Core.Application.InboundPorts;
public interface IGoalService
{
    public abstract Task<List<GoalEntity>> ReadAllGoals();
    public abstract Task<GoalEntity> ReadGoalById(int id);
    public abstract Task<GoalEntity> CreateGoal(GoalEntity goalEntity);
    public abstract Task DeleteGoal(int id);
    public abstract Task UpdateGoal(GoalEntity goalEntity);
}
