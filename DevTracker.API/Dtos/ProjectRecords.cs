namespace DevTracker.API.Models;
public record CreateProjectDto(int? GoalId, string Title, string? Description, string CurrentStatus);
public record GetProjectSummaryDto(int Id, string Title, string? Description, string CurrentStatus);
public record GetProjectDto(GetProjectSummaryDto Summary, List<GetSessionDto> CodingSessions, List<GetSessionDto> TheorySessions);
public record UpdateProjectDto(string? Title, string? Description, string? CurrentStatus);
