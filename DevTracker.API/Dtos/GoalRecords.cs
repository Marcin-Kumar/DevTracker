using DevTracker.API.Dtos.Validations;
using DevTracker.Core.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.API.Models;

public record CreateGoalDto([StringLength(100, MinimumLength = 3)] string Title, [StringLength(300)] string? Description, DateTime CreatedAt, DateTime AchieveBy,[StringLength(500)] string? Notes, Status CurrentStatus,[LessThan24Hours] TimeSpan DailyTargetHours);
public record GetGoalDto(GetGoalSummaryDto summary, List<GetProjectDto> Projects, List<GetSessionDto> CodingSessions, List<GetSessionDto> TheorySessions);
public record GetGoalSummaryDto(int Id, string Title, string? Description, DateTime CreatedAt, DateTime AchieveBy, string? Notes, Status CurrentStatus, TimeSpan DailyTargetHours);
public record UpdateGoalDto([StringLength(100, MinimumLength = 3)] string? Title,[StringLength(300)] string? Description, DateTime? CreatedAt, DateTime? AchieveBy,[StringLength(500)] string? Notes, Status? CurrentStatus,[LessThan24Hours] TimeSpan? DailyTargetHours);
