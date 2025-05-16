namespace DevTracker.Domain.Entities;

public class GoalEntity
{
    public int? Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; set; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime AchieveBy { get; init; }
    public string? Notes { get; set; }
    public List<ProjectEntity>? Projects { get; init; }
    public Status CurrentStatus { get; set; } = Status.Planned;
    public TimeSpan DailyTargetHours { get; set; }
    public List<SessionEntity>? CodingSessions { get; set; }
    public List<SessionEntity>? TheorySessions { get; set; }
}