using DevTracker.Core.Domain.Entities;
using DevTracker.Data.Models;

namespace DevTracker.Data.Mappers;

public class ProjectDataMapper
{
    private readonly SessionDataMapper _sessionMapper;

    public ProjectDataMapper(SessionDataMapper sessionMapper)
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
            CodingSessions = p.CodingSessions.ConvertAll(_sessionMapper.ToModel),
            TheorySessions = p.TheorySessions.ConvertAll(_sessionMapper.ToModel),
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
            CodingSessions = p.CodingSessions.ConvertAll(_sessionMapper.ToEntity),
            TheorySessions = p.TheorySessions.ConvertAll(_sessionMapper.ToEntity),
        };
    }
}