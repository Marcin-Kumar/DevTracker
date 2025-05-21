namespace DevTracker.API.Models;

public record CreateSessionDto(int? GoalId, int? ProjectId, string Type, string? Notes, string Title, DateTime StartedAtDateTime, DateTime? EndedAtDateTime);
public record GetSessionDto(int Id, string Type, string? Notes, string Title, DateTime StartedAtDateTime, DateTime? EndedAtDateTime);
public record UpdateSessionDto(string? Type, string? Notes, string? Title, DateTime? StartedAtDateTime, DateTime? EndedAtDateTime);
