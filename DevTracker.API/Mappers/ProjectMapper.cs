using DevTracker.API.Models;
using DevTracker.Domain.Entities;

namespace DevTracker.API.Mappers;

public class ProjectMapper
{
    private readonly SessionMapper _sessionSummaryMapper;

    public ProjectMapper(SessionMapper sessionSummaryMapper)
    {
        _sessionSummaryMapper = sessionSummaryMapper;
    }

    internal GetProjectDto ToGetProjectDto(ProjectEntity p) => new GetProjectDto
    {
        summary = ToGetProjectSummaryDto(p),
        CodingSessions = p.CodingSessions?.Select(_sessionSummaryMapper.ToGetSessionDto).ToList() ?? [],
        TheorySessions = p.TheorySessions?.Select(_sessionSummaryMapper.ToGetSessionDto).ToList() ?? [],
    };

    internal GetProjectSummaryDto ToGetProjectSummaryDto(ProjectEntity p) => new GetProjectSummaryDto
    {
        Id = (int)p.Id!,
        Title = p.Title,
        Description = p.Description,
        CurrentStatus = p.CurrentStatus.ToString(),
    };

    internal ProjectEntity toEntity(CreateProjectDto p)
    {
        Status status;
        Enum.TryParse<Status>(p.CurrentStatus, ignoreCase: true, out status);
        return new ProjectEntity
        {
            Title = p.Title,
            Description = p.Description,
            CurrentStatus = status,
        };
    }

    internal ProjectEntity toEntity(ProjectEntity e, UpdateProjectDto p)
    {
        Status status;
        Enum.TryParse<Status>(p.CurrentStatus, ignoreCase: true, out status);
        return new ProjectEntity
        {
            Id = e.Id,
            Title = p.Title ?? e.Title,
            Description = p.Description ?? e.Description,
            CurrentStatus = p.CurrentStatus == null ? e.CurrentStatus : status,
        };
    }
}
