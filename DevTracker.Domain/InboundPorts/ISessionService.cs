using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
public interface ISessionService
{
    public abstract Task<List<SessionEntity>> ReadAllSessions();
    public abstract Task<List<SessionEntity>> ReadAllCodingSessions();
    public abstract Task<List<SessionEntity>> ReadAllTheorySessions();
    public abstract Task CreateSessionForGoalWithId(int goalId, SessionEntity session);
    public abstract Task CreateSessionForProjectWithId(int projectId, SessionEntity session);
    public abstract Task DeleteSession(int id);
    public abstract Task UpdateSession(SessionEntity session);
}

