using DevTracker.Core.Domain.Entities.Enums;

namespace DevTracker.Data.Models;
public class Project
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? GoalId { get; set; }
    public Goal? Goal { get; set; }
    public Status CurrentStatus { get; set; }
    public List<Session> CodingSessions { get; set; } = new List<Session>();
    public List<Session> TheorySessions { get; set; } = new List<Session>();
}
