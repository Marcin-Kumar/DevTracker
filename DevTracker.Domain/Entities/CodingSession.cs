namespace DevTracker.Domain.Entities;

public class CodingSession : Session
{
    public CodingSession(string notes, DateTime startedAtDateTime) : base(notes, startedAtDateTime)
    {
    }
}