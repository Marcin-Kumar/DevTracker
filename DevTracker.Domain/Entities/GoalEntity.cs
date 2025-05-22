using DevTracker.Domain.Entities.Enums;

namespace DevTracker.Domain.Entities;

public class GoalEntity
{
    public int? Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime AchieveBy { get; init; }
    public string? Notes { get; init; }
    public Status CurrentStatus { get; init; } = Status.Planned;
    public TimeSpan DailyTargetHours { get; init; }
    public List<ProjectEntity> Projects { get; init; } = [];
    public List<SessionEntity> CodingSessions { get; set; } = [];
    public List<SessionEntity> TheorySessions { get; set; } = [];

    internal void AddSession(SessionEntity session)
    {
        if (session.Type == SessionType.Coding)
        {
            CodingSessions.Add(session);
        }
        else if (session.Type == SessionType.Theory)
        {
            TheorySessions.Add(session);
        }
    }
}