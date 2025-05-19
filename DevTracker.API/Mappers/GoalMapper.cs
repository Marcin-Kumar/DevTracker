using DevTracker.API.Models;
using DevTracker.Domain.Entities;

namespace DevTracker.API.Mappers;

public class GoalMapper
{
    private readonly ProjectMapper _projectMapper;
    private readonly SessionMapper _sessionSummaryMapper;

    internal GoalMapper(ProjectMapper projectMapper, SessionMapper sessionSummaryMapper)
    {
        _projectMapper = projectMapper;
        _sessionSummaryMapper = sessionSummaryMapper;
    }

    internal GetGoalDto ToGetGoalDto(GoalEntity g) => new GetGoalDto
    {
        summary = ToGetGoalSummaryDto(g),
        Projects = g.Projects?.Select(_projectMapper.ToGetProjectDto).ToList() ?? [],
        CodingSessions = g.CodingSessions?.Select(_sessionSummaryMapper.ToGetSessionDto).ToList() ?? new List<GetSessionDto>(),
        TheorySessions = g.TheorySessions?.Select(_sessionSummaryMapper.ToGetSessionDto).ToList() ?? new List<GetSessionDto>(),
    };

    internal GetGoalSummaryDto ToGetGoalSummaryDto(GoalEntity g) => new GetGoalSummaryDto
    {
        Id = (int)g.Id!,
        Title = g.Title,
        Description = g.Description,
        AchieveBy = g.AchieveBy,
        CreatedAt = g.CreatedAt,
        CurrentStatus = g.CurrentStatus.ToString(),
        DailyTargetHours = g.DailyTargetHours,
        Notes = g.Notes,
    };


    internal GoalEntity toEntity(CreateGoalDto g)
    {
        Status status;
        Enum.TryParse<Status>(g.CurrentStatus, ignoreCase: true, out status);
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

    internal GoalEntity toEntity(GoalEntity e, UpdateGoalDto g)
    {
        Status status;
        Enum.TryParse<Status>(g.CurrentStatus, ignoreCase: true, out status);
        return new GoalEntity
        {
            Id = e.Id,
            Title = g.Title ?? e.Title,
            Description = g.Description ?? e.Description,
            CreatedAt = g.CreatedAt ?? e.CreatedAt,
            AchieveBy = g.AchieveBy ?? e.AchieveBy,
            Notes = g.Notes ?? e.Notes,
            CurrentStatus = g.CurrentStatus == null ? e.CurrentStatus : status,
            DailyTargetHours = g.DailyTargetHours ?? e.DailyTargetHours,
        };
    }
}
