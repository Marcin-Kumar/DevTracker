namespace DevTracker.Domain;

public class TheorySession : Session
{
    public TheorySession(string notes, DateTime startedAtDateTime) : base(notes, startedAtDateTime) 
    {
    }
}