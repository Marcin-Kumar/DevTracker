using DevTracker.Data.Models;
using DevTracker.Domain.Entities;

namespace DevTracker.Data.Mappers;

internal class ProjectMapper
{
    private readonly SessionMapper _sessionMapper;

    public ProjectMapper(SessionMapper sessionMapper)
    {
        _sessionMapper = sessionMapper;
    }

    internal Project ToModel(ProjectEntity p)
    {
        return new Project
        {
            Id = p.Id ?? 0,
            Title = p.Title,
            Description = p.Description,
            CurrentStatus = p.CurrentStatus,
            CodingSessions = p.CodingSessions?.Select(_sessionMapper.ToModel).ToList(),
            TheorySessions = p.TheorySessions?.Select(_sessionMapper.ToModel).ToList(),
        };
    }

    internal ProjectEntity ToEntity(Project p)
    {
        return new ProjectEntity
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            CurrentStatus = p.CurrentStatus,
            CodingSessions = p.CodingSessions?.Select(_sessionMapper.ToEntity).ToList(),
            TheorySessions = p.TheorySessions?.Select(_sessionMapper.ToEntity).ToList(),
        };
    }
}