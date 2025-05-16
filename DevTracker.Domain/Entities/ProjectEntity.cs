namespace DevTracker.Domain.Entities;

public class ProjectEntity
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status CurrentStatus { get; set; }
    public List<SessionEntity>? CodingSessions { get; init; }
    public List<SessionEntity>? TheorySessions { get; init; }
}