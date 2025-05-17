using DevTracker.Domain.Entities;
using DevTracker.Domain.InboundPorts;
using DevTracker.Domain.Ports;

namespace DevTracker.Domain.Application;
public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IGoalRepository _goalRepository;
    private readonly IProjectRepository _projectRepository;

    public SessionService(ISessionRepository sessionRepository, IGoalRepository goalRepository, IProjectRepository projectRepository)
    {
        _sessionRepository = sessionRepository;
        _goalRepository = goalRepository;
        _projectRepository = projectRepository;
    }

    public async Task CreateSessionForGoalWithId(int goalId, SessionEntity session)
    {
        GoalEntity goal = await _goalRepository.ReadGoalById(goalId);
        goal.AddSession(session);
        await _goalRepository.UpdateGoal(goal);
    }

    public async Task CreateSessionForProjectWithId(int projectId, SessionEntity session)
    {
        ProjectEntity project = await _projectRepository.ReadProject(projectId);
        project.AddSession(session);
        await _projectRepository.UpdateProject(project);
    }


    public async Task DeleteSession(int id)
    {
        await _sessionRepository.DeleteSession(id);
    }

    public async Task<List<SessionEntity>> ReadAllSessions()
    {
        return await _sessionRepository.ReadAllSessions();
    }

    public async Task<List<SessionEntity>> ReadAllCodingSessions()
    {
        return await _sessionRepository.ReadAllCodingSessions();
    }

    public async Task<List<SessionEntity>> ReadAllTheorySessions()
    {
        return await _sessionRepository.ReadAllTheorySessions();
    }

    public async Task UpdateSession(SessionEntity session)
    {
        await _sessionRepository.UpdateSession(session);
    }
}
