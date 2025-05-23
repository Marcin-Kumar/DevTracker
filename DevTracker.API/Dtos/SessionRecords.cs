using DevTracker.Core.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.API.Models;

public record CreateSessionDto(int? GoalId, int? ProjectId, SessionType Type, [StringLength(300)] string? Notes,[StringLength(100, MinimumLength = 3)] string Title, DateTime StartedAtDateTime, DateTime? EndedAtDateTime);
public record GetSessionDto(int Id, SessionType Type, string? Notes, string Title, DateTime StartedAtDateTime, DateTime? EndedAtDateTime);
public record UpdateSessionDto(SessionType? Type, [StringLength(300)] string? Notes,[StringLength(100, MinimumLength = 3)] string? Title, DateTime? StartedAtDateTime, DateTime? EndedAtDateTime);
