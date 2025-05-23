using DevTracker.Core.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.API.Models;
public record CreateProjectDto(int? GoalId,[StringLength(100, MinimumLength = 3)] string Title,[StringLength(300)] string? Description, Status CurrentStatus);
public record GetProjectSummaryDto(int Id, string Title, string? Description, Status CurrentStatus);
public record GetProjectDto(GetProjectSummaryDto Summary, List<GetSessionDto> CodingSessions, List<GetSessionDto> TheorySessions);
public record UpdateProjectDto([StringLength(100, MinimumLength = 3)] string? Title, [StringLength(300)] string? Description, Status? CurrentStatus);
