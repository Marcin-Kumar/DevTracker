namespace DevTracker.Domain.Entities;

public class Session
{
    public Session(string notes, DateTime startedAtDateTime)
    {
        Notes = notes;
        StartedAtDateTime = startedAtDateTime;
    }

    public TimeSpan Duration => EndedAtDateTime - StartedAtDateTime; 

    public Guid Id { get; init; }
    public string Notes { get; set; } = string.Empty;
    public required string Title { get; set; }
    public required DateTime StartedAtDateTime { get; set; }
    public DateTime EndedAtDateTime { get; set; }
}