namespace DevTracker.Domain.Entities;

public class GoalEntity
{
    public GoalEntity(DateTime? createdAt, string title, DateTime? achieveBy)
    {
        CreatedAt = createdAt;
        Title = title;
        AchieveBy = achieveBy;
    }

    public List<SessionEntity> CodingSessions { get; init; } = new();
    public required DateTime? CreatedAt { get; init; }
    public required DateTime? AchieveBy { get; init; }
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; init; }
    public string Notes { get; set; } = string.Empty;
    public List<ProjectEntity> Projects { get; init; } = new();
    public Status Status { get; set; } = Status.Planned;
    public TimeSpan? DailyTargetHours { get; set; }
    public List<SessionEntity> TheorySessions { get; init; } = new();
    public required string Title { get; init; }
}