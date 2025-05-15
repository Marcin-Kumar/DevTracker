using DevTracker.Domain.Entities.Enums;

namespace DevTracker.Data.Models;
internal class Session
{
    public SessionType SessionType { get; set; }
    public int Id { get; set; }
    public string Notes { get; set; }
    public string Title { get; set; }
    public DateTime StartedAtDateTime { get; set; }
    public DateTime EndedAtDateTime { get; set; }
}
