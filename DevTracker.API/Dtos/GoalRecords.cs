namespace DevTracker.API.Models;

public record struct GetGoalDto(GetGoalSummaryDto summary, List<GetProjectDto> Projects, List<GetSessionDto> CodingSessions, List<GetSessionDto> TheorySessions);
public record struct GetGoalSummaryDto(int Id, string Title, string? Description, DateTime CreatedAt, DateTime AchieveBy, string? Notes, string CurrentStatus, TimeSpan DailyTargetHours);
public record struct CreateGoalDto(string Title, string? Description, DateTime CreatedAt, DateTime AchieveBy, string? Notes, string CurrentStatus, TimeSpan DailyTargetHours);
public record struct UpdateGoalDto(string? Title, string? Description, DateTime? CreatedAt, DateTime? AchieveBy, string? Notes, string? CurrentStatus, TimeSpan? DailyTargetHours);
