namespace DevTracker.API.Models;

public record struct GetProjectSummaryDto(int Id, string Title, string? Description, string CurrentStatus);
public record struct GetProjectDto(GetProjectSummaryDto summary, List<GetSessionDto> CodingSessions, List<GetSessionDto> TheorySessions);
public record struct CreateProjectDto(int? goalId, string Title, string? Description, string CurrentStatus);
public record struct UpdateProjectDto(string? Title, string? Description, string? CurrentStatus);
