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

    public async Task<SessionEntity> CreateSessionForGoalWithId(int goalId, SessionEntity session)
    {
        GoalEntity goal = await _goalRepository.ReadGoalById(goalId);
        goal.AddSession(session);
        return await _sessionRepository.CreateSession(session);
    }

    public async Task<SessionEntity> CreateSessionForProjectWithId(int projectId, SessionEntity session)
    {
        ProjectEntity project = await _projectRepository.ReadProject(projectId);
        project.AddSession(session);
        return await _sessionRepository.CreateSession(session);
    }


    public async Task DeleteSession(int id)
    {
        await _sessionRepository.DeleteSession(id);
    }

    public async Task<List<SessionEntity>> ReadAllSessions()
    {
        return await _sessionRepository.ReadAllSessions();
    }

    public async Task<SessionEntity> ReadSessionWithId(int id)
    {
        return await _sessionRepository.ReadSessionWithId(id);
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
