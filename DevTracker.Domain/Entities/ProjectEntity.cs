namespace DevTracker.Domain.Entities;

public class ProjectEntity
{
    public List<SessionEntity> CodingSessions { get; init; } = new();
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public List<SessionEntity> TheorySessions { get; init; } = new();
}