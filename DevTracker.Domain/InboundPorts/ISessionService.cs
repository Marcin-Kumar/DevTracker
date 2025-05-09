using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
public interface ISessionService
{
    public abstract List<Session> GetAllSessions();
    public abstract List<Session> GetAllCodingSessions();
    public abstract List<Session> GetAllTheorySessions();
    public abstract Session GetSessionById();
    public abstract void CreateSession();
    public abstract void DeleteSession();
    public abstract void UpdateSession();
}

