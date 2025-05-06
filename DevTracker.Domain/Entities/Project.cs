namespace DevTracker.Domain.Entities;

public class Project
{
    public List<CodingSession> CodingSessions { get; init; } = new();
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public List<TheorySession> TheorySessions { get; init; } = new();
}