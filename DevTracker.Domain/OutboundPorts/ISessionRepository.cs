using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface ISessionRepository
{
    public abstract Task CreateSession(SessionEntity session);
    public abstract Task UpdateSession(SessionEntity session);
    public abstract Task DeleteSession(int id);
    public abstract Task<List<SessionEntity>> ReadAllSessions();
}
