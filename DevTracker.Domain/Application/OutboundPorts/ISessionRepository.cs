using DevTracker.Core.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface ISessionRepository
{
    public abstract Task<SessionEntity> CreateSession(SessionEntity session);
    public abstract Task UpdateSession(SessionEntity session);
    public abstract Task DeleteSession(int id);
    public abstract Task<List<SessionEntity>> ReadAllSessions();
    public abstract Task<List<SessionEntity>> ReadAllCodingSessions();
    public abstract Task<List<SessionEntity>> ReadAllTheorySessions();
    public abstract Task<SessionEntity> ReadSessionWithId(int id);
}
