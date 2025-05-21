namespace DevTracker.API.Models;

public record CreateGoalDto(string Title, string? Description, DateTime CreatedAt, DateTime AchieveBy, string? Notes, string CurrentStatus, TimeSpan DailyTargetHours);
public record GetGoalDto(GetGoalSummaryDto summary, List<GetProjectDto> Projects, List<GetSessionDto> CodingSessions, List<GetSessionDto> TheorySessions);
public record GetGoalSummaryDto(int Id, string Title, string? Description, DateTime CreatedAt, DateTime AchieveBy, string? Notes, string CurrentStatus, TimeSpan DailyTargetHours);
public record UpdateGoalDto(string? Title, string? Description, DateTime? CreatedAt, DateTime? AchieveBy, string? Notes, string? CurrentStatus, TimeSpan? DailyTargetHours);
