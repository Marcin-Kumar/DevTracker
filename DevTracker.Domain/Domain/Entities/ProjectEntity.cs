using DevTracker.Core.Domain.Entities.Enums;

namespace DevTracker.Core.Domain.Entities;

public class ProjectEntity
{
    public int? Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public Status CurrentStatus { get; init; }
    public List<SessionEntity> CodingSessions { get; set; } = [];
    public List<SessionEntity> TheorySessions { get; set; } = [];

    internal void AddSession(SessionEntity session)
    {
        if(session.Type == SessionType.Coding)
        {
            CodingSessions.Add(session);
        }
        else if (session.Type == SessionType.Theory)
        {
            TheorySessions.Add(session);
        }
    }
}