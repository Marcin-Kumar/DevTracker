namespace DevTracker.Data.Models;
public class Project
{
    public List<Session> CodingSessions { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Session> TheorySessions { get; set; }
}
