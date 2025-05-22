using DevTracker.API.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Entities.Enums;

namespace DevTracker.API.Mappers;

public class SessionDtoMapper
{
    internal GetSessionDto ToGetSessionDto(SessionEntity s)
    {
        return new GetSessionDto (
            Id: (int)s.Id!,
            Type: s.Type.ToString(),
            Notes: s.Notes,
            Title: s.Title,
            StartedAtDateTime: s.StartedAtDateTime,
            EndedAtDateTime: s.EndedAtDateTime
        );
    }

    internal SessionEntity ToEntity(CreateSessionDto s)
    {
        Enum.TryParse<SessionType>(s.Type, ignoreCase: true, out SessionType sessionType);
        return new SessionEntity {
            Type = sessionType,
            Title = s.Title,
            Notes = s.Notes,
            StartedAtDateTime = s.StartedAtDateTime,
            EndedAtDateTime = s.EndedAtDateTime
        };
    }

    internal SessionEntity ToEntity(SessionEntity e, UpdateSessionDto s)
    {
        Enum.TryParse<SessionType>(s.Type, ignoreCase: true, out SessionType sessionType);
        return new SessionEntity {
            Id = e.Id,
            Type = s.Type is not null ? sessionType : e.Type,
            Title = s.Title ?? e.Title,
            Notes = s.Notes ?? e.Notes,
            StartedAtDateTime = s.StartedAtDateTime ?? e.StartedAtDateTime,
            EndedAtDateTime = s.EndedAtDateTime ?? e.EndedAtDateTime,
        };
    }
}