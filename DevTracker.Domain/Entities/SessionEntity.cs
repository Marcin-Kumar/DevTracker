using DevTracker.Domain.Entities.Enums;
namespace DevTracker.Domain.Entities;

public class SessionEntity
{
    public int? Id { get; init; }
    public SessionType Type { get; set; }
    public string? Notes { get; set; }
    public required string Title { get; set; }
    public required DateTime StartedAtDateTime { get; set; }
    public DateTime? EndedAtDateTime { get; set; }
    public TimeSpan Duration => (EndedAtDateTime ?? DateTime.UtcNow) - StartedAtDateTime;
}