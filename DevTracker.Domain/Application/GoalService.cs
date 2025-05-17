using DevTracker.Domain.Entities;
using DevTracker.Domain.InboundPorts;
using DevTracker.Domain.Ports;

namespace DevTracker.Domain.Application;
internal class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;
    
    public GoalService(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task CreateGoal(GoalEntity goalEntity)
    {
        await _goalRepository.CreateGoal(goalEntity);
    }

    public async Task DeleteGoal(int id)
    {
        await _goalRepository.DeleteGoal(id);
    }

    public async Task<GoalEntity> ReadGoalById(int id)
    {
         return await _goalRepository.ReadGoalById(id);
    }

    public async Task<List<GoalEntity>> ReadAllGoals()
    {
        return await _goalRepository.ReadAllGoals();
    }

    public Task UpdateGoal(GoalEntity goalEntity)
    {
        return _goalRepository.UpdateGoal(goalEntity);
    }
}
