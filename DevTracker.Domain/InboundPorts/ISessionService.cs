using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
public interface ISessionService
{
    public abstract List<SessionEntity> GetAllSessions();
    public abstract List<SessionEntity> GetAllCodingSessions();
    public abstract List<SessionEntity> GetAllTheorySessions();
    public abstract SessionEntity GetSessionById();
    public abstract void CreateSession();
    public abstract void DeleteSession();
    public abstract void UpdateSession();
}

