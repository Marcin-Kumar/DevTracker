using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface ISessionRepository
{
    public abstract void CreateSession(Session session);
    public abstract void UpdateSession(Session session);
    public abstract void DeleteSession(Session id);
    public abstract void ReadAllSessions();
}
