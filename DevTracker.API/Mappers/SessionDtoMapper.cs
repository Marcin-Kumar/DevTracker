using DevTracker.API.Models;
using DevTracker.Core.Domain.Entities;

namespace DevTracker.API.Mappers;

public class SessionDtoMapper
{
    internal GetSessionDto ToGetSessionDto(SessionEntity s)
    {
        return new GetSessionDto (
            Id: (int)s.Id!,
            Type: s.Type,
            Notes: s.Notes,
            Title: s.Title,
            StartedAtDateTime: s.StartedAtDateTime,
            EndedAtDateTime: s.EndedAtDateTime
        );
    }

    internal SessionEntity ToEntity(CreateSessionDto s)
    {
        return new SessionEntity {
            Type = s.Type,
            Title = s.Title,
            Notes = s.Notes,
            StartedAtDateTime = s.StartedAtDateTime,
            EndedAtDateTime = s.EndedAtDateTime
        };
    }

    internal SessionEntity ToEntity(SessionEntity e, UpdateSessionDto s)
    {
        return new SessionEntity {
            Id = e.Id,
            Type = s.Type ?? e.Type,
            Title = s.Title ?? e.Title,
            Notes = s.Notes ?? e.Notes,
            StartedAtDateTime = s.StartedAtDateTime ?? e.StartedAtDateTime,
            EndedAtDateTime = s.EndedAtDateTime ?? e.EndedAtDateTime,
        };
    }
}