using DevTracker.API.Models;
using DevTracker.Core.Domain.Entities;
using DevTracker.Core.Domain.Entities.Enums;

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
        Summary: ToGetProjectSummaryDto(p),
        CodingSessions: p.CodingSessions.ConvertAll(_sessionMapper.ToGetSessionDto),
        TheorySessions: p.TheorySessions.ConvertAll(_sessionMapper.ToGetSessionDto)
    );

    internal GetProjectSummaryDto ToGetProjectSummaryDto(ProjectEntity p) => new GetProjectSummaryDto
    (
        Id: (int)p.Id!,
        Title: p.Title,
        Description: p.Description,
        CurrentStatus: p.CurrentStatus.ToString()
    );

    internal ProjectEntity ToEntity(CreateProjectDto p)
    {
        Enum.TryParse<Status>(p.CurrentStatus, ignoreCase: true, out Status status);
        return new ProjectEntity
        {
            Title = p.Title,
            Description = p.Description,
            CurrentStatus = status,
        };
    }

    internal ProjectEntity ToEntity(ProjectEntity e, UpdateProjectDto p)
    {
        Enum.TryParse<Status>(p.CurrentStatus, ignoreCase: true, out Status status);
        return new ProjectEntity
        {
            Id = e.Id,
            Title = p.Title ?? e.Title,
            Description = p.Description ?? e.Description,
            CurrentStatus = p.CurrentStatus is null ? e.CurrentStatus : status,
        };
    }
}
