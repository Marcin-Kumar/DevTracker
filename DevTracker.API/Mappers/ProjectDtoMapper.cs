using DevTracker.API.Models;
using DevTracker.Domain.Entities;

namespace DevTracker.API.Mappers;

public class ProjectDtoMapper
{
    private readonly SessionDtoMapper _sessionMapper;

    public ProjectDtoMapper(SessionDtoMapper sessionMapper)
    {
        _sessionMapper = sessionMapper;
    }

    internal GetProjectDto ToGetProjectDto(ProjectEntity p) => new GetProjectDto
    (
        summary: ToGetProjectSummaryDto(p),
        CodingSessions: p.CodingSessions?.Select(_sessionMapper.ToGetSessionDto).ToList() ?? [],
        TheorySessions: p.TheorySessions?.Select(_sessionMapper.ToGetSessionDto).ToList() ?? []
    );

    internal GetProjectSummaryDto ToGetProjectSummaryDto(ProjectEntity p) => new GetProjectSummaryDto
    (
        Id: (int)p.Id!,
        Title: p.Title,
        Description: p.Description,
        CurrentStatus: p.CurrentStatus.ToString()
    );

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
