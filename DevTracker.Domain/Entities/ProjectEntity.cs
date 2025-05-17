
using DevTracker.Domain.Entities.Enums;

namespace DevTracker.Domain.Entities;

public class ProjectEntity
{
    public int? Id { get; init; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status CurrentStatus { get; set; }
    public List<SessionEntity>? CodingSessions { get; set; }
    public List<SessionEntity>? TheorySessions { get; set; }

    internal void AddSession(SessionEntity session)
    {
        if(session.Type == SessionType.Coding)
        {
            CodingSessions ??= new List<SessionEntity>();
            CodingSessions.Add(session);
        }
        else if (session.Type == SessionType.Theory)
        {
            TheorySessions ??= new List<SessionEntity>();
            TheorySessions.Add(session);
        }
    }
}