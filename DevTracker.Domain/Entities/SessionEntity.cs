using DevTracker.Domain.Entities.Enums;
namespace DevTracker.Domain.Entities;

public class SessionEntity
{
    public int? Id { get; init; }
    public SessionType Type { get; init; }
    public string? Notes { get; init; }
    public required string Title { get; init; }
    public required DateTime StartedAtDateTime { get; init; }
    public DateTime? EndedAtDateTime { get; init; }
    public TimeSpan Duration => (EndedAtDateTime ?? DateTime.UtcNow) - StartedAtDateTime;
}