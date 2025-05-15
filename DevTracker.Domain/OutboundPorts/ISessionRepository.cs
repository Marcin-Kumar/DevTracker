using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface ISessionRepository
{
    public abstract void CreateSession(SessionEntity session);
    public abstract void UpdateSession(SessionEntity session);
    public abstract void DeleteSession(SessionEntity id);
    public abstract void ReadAllSessions();
}
