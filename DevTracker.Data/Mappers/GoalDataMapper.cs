using DevTracker.Core.Domain.Entities;
using DevTracker.Data.Models;

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
            Projects = g.Projects.ConvertAll(_internalProjectMapper.ToModel),
            CurrentStatus = g.CurrentStatus,
            CodingSessions = g.CodingSessions.ConvertAll(_internalSessionMapper.ToModel),
            TheorySessions = g.TheorySessions.ConvertAll(_internalSessionMapper.ToModel)
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
            Projects = g.Projects.ConvertAll(_internalProjectMapper.ToEntity),
            CurrentStatus = g.CurrentStatus,
            CodingSessions = g.CodingSessions.ConvertAll(_internalSessionMapper.ToEntity),
            TheorySessions = g.TheorySessions.ConvertAll(_internalSessionMapper.ToEntity)
        };
    }
}
