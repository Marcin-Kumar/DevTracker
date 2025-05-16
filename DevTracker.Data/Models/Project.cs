using DevTracker.Domain.Entities;

namespace DevTracker.Data.Models;
public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int? GoalId { get; set; }
    public Goal? Goal { get; set; }
    public Status CurrentStatus { get; set; }
    public List<Session>? CodingSessions { get; set; }
    public List<Session>? TheorySessions { get; set; }
}
