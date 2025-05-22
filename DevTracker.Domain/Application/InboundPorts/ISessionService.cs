using DevTracker.Core.Domain.Entities;

namespace DevTracker.Core.Application.InboundPorts;
public interface ISessionService
{
    public abstract Task<SessionEntity> CreateSessionForGoalWithId(int goalId, SessionEntity session);
    public abstract Task<SessionEntity> CreateSessionForProjectWithId(int projectId, SessionEntity session);
    public abstract Task<List<SessionEntity>> ReadAllSessions();
    public abstract Task<List<SessionEntity>> ReadAllCodingSessions();
    public abstract Task<List<SessionEntity>> ReadAllTheorySessions();
    public abstract Task<SessionEntity> ReadSessionWithId(int id);
    public abstract Task UpdateSession(SessionEntity session);
    public abstract Task DeleteSession(int id);
}

