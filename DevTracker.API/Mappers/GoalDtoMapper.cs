using DevTracker.API.Models;
using DevTracker.Core.Domain.Entities;
using DevTracker.Core.Domain.Entities.Enums;

namespace DevTracker.API.Mappers;

public class GoalDtoMapper
{
    private readonly ProjectDtoMapper _projectMapper;
    private readonly SessionDtoMapper _sessionMapper;

    public GoalDtoMapper(ProjectDtoMapper projectMapper, SessionDtoMapper sessionMapper)
    {
        _projectMapper = projectMapper;
        _sessionMapper = sessionMapper;
    }

    internal GetGoalDto ToGetGoalDto(GoalEntity g) => new GetGoalDto
    (
        summary: ToGetGoalSummaryDto(g),
        Projects: g.Projects.ConvertAll(_projectMapper.ToGetProjectDto),
        CodingSessions: g.CodingSessions.ConvertAll(_sessionMapper.ToGetSessionDto),
        TheorySessions: g.TheorySessions.ConvertAll(_sessionMapper.ToGetSessionDto)
    );

    internal GetGoalSummaryDto ToGetGoalSummaryDto(GoalEntity g) => new GetGoalSummaryDto
    (
        Id: (int)g.Id!,
        Title: g.Title,
        Description: g.Description,
        CreatedAt: g.CreatedAt,
        AchieveBy: g.AchieveBy,
        Notes: g.Notes,
        CurrentStatus: g.CurrentStatus.ToString(),
        DailyTargetHours: g.DailyTargetHours
    );

    internal GoalEntity ToEntity(CreateGoalDto g)
    {
        Enum.TryParse<Status>(g.CurrentStatus, ignoreCase: true, out Status status);
        return new GoalEntity
        {
            Title = g.Title,
            Description = g.Description,
            CreatedAt = g.CreatedAt,
            AchieveBy = g.AchieveBy,
            Notes = g.Notes,
            CurrentStatus = status,
            DailyTargetHours = g.DailyTargetHours,
        };
    }

    internal GoalEntity ToEntity(GoalEntity e, UpdateGoalDto g)
    {
        Enum.TryParse<Status>(g.CurrentStatus, ignoreCase: true, out Status status);
        return new GoalEntity
        {
            Id = e.Id,
            Title = g.Title ?? e.Title,
            Description = g.Description ?? e.Description,
            CreatedAt = g.CreatedAt ?? e.CreatedAt,
            AchieveBy = g.AchieveBy ?? e.AchieveBy,
            Notes = g.Notes ?? e.Notes,
            CurrentStatus = g.CurrentStatus is null ? e.CurrentStatus : status,
            DailyTargetHours = g.DailyTargetHours ?? e.DailyTargetHours,
        };
    }
}
