using DevTracker.Domain.Entities.Enums;

namespace DevTracker.Domain.Entities;

public class Session
{
    public Session(string notes, DateTime startedAtDateTime, SessionType sessionType)
    {
        Notes = notes;
        StartedAtDateTime = startedAtDateTime;
        SessionType = sessionType;
    }

    public TimeSpan Duration => EndedAtDateTime - StartedAtDateTime;
    public required SessionType SessionType { get; init; }
    public Guid Id { get; init; }
    public string Notes { get; set; } = string.Empty;
    public required string Title { get; set; }
    public required DateTime StartedAtDateTime { get; set; }
    public DateTime EndedAtDateTime { get; set; }
}