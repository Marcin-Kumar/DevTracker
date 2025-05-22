using DevTracker.Data.Models;
using DevTracker.Domain.Entities;

namespace DevTracker.Data.Mappers;
public class GoalDataMapper
{
    private readonly ProjectDataMapper _internalProjectMapper;
    private readonly SessionDataMapper _internalSessionMapper;
    public GoalDataMapper(ProjectDataMapper internalProjectMapper, SessionDataMapper internalSessionMapper)
    {
        _internalProjectMapper = internalProjectMapper;
        _internalSessionMapper = internalSessionMapper;
    }

    internal Goal ToModel(GoalEntity g)
    {
        return new Goal
        {
            Id = g.Id ?? 0,
            Title = g.Title,
            Description = g.Description,
            Notes = g.Notes,
            AchieveBy = g.AchieveBy,
            CreatedAt = g.CreatedAt,
            DailyTargetHours = g.DailyTargetHours,
            Projects = g.Projects.Select(_internalProjectMapper.ToModel).ToList(),
            CurrentStatus = g.CurrentStatus,
            CodingSessions = g.CodingSessions.Select(_internalSessionMapper.ToModel).ToList(),
            TheorySessions = g.TheorySessions.Select(_internalSessionMapper.ToModel).ToList()
        };
    }

    internal GoalEntity ToEntity(Goal g)
    {
        return new GoalEntity
        {
            Id = g.Id,
            Title = g.Title,
            Description = g.Description,
            Notes = g.Notes,
            AchieveBy = g.AchieveBy,
            CreatedAt = g.CreatedAt,
            DailyTargetHours = g.DailyTargetHours,
            Projects = g.Projects.Select(_internalProjectMapper.ToEntity).ToList(),
            CurrentStatus = g.CurrentStatus,
            CodingSessions = g.CodingSessions.Select(_internalSessionMapper.ToEntity).ToList(),
            TheorySessions = g.TheorySessions.Select(_internalSessionMapper.ToEntity).ToList()
        };
    }
}
