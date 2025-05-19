namespace DevTracker.API.Models;

public record struct GetSessionDto(int Id, string Type, string? Notes, string Title, DateTime StartedAtDateTime, DateTime? EndedAtDateTime);
public record struct CreateSessionDto(int? GoalId, int? ProjectId, string Type, string? Notes, string Title, DateTime StartedAtDateTime, DateTime? EndedAtDateTime);
public record struct UpdateSessionDto(string? Type, string? Notes, string? Title, DateTime? StartedAtDateTime, DateTime? EndedAtDateTime);
