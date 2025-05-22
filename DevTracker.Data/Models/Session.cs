using DevTracker.Domain.Entities.Enums;

namespace DevTracker.Data.Models;
public class Session
{
    public int Id { get; set; }
    public string? Notes { get; set; }
    public required string Title { get; set; }
    public SessionType Type { get; set; }
    public DateTime StartedAtDateTime { get; set; }
    public DateTime? EndedAtDateTime { get; set; }
    public int? GoalCodingSessionId { get; set; }
    public Goal? GoalCodingSession { get; set; }
    public int? GoalTheorySessionId { get; set; }
    public Goal? GoalTheorySession { get; set; }
    public int? ProjectCodingSessionId { get; set; }
    public Project? ProjectCodingSession { get; set; }
    public int? ProjectTheorySessionId { get; set; }
    public Project? ProjectTheorySession { get; set; }
}
