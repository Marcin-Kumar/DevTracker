using DevTracker.Domain.Entities;

namespace DevTracker.Data.Models;

internal class Goal
{
    public DateTime AchieveBy { get; set; }
    public List<Session> CodingSessions { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan DailyTargetHours { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
    public string Notes { get; set; }
    public List<Project> Projects { get; set; }
    public Status Status { get; set; }
    public List<Session> TheorySessions { get; set; }
    public string Title { get; set; }
}