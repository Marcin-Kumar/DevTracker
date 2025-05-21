using DevTracker.Data.Models;
using DevTracker.Domain.Entities;

namespace DevTracker.Data.Mappers;

public class SessionDataMapper
{
    internal Session ToModel(SessionEntity s)
    {
        return new Session
        {
            Id = s.Id ?? 0,
            EndedAtDateTime = s.EndedAtDateTime,
            Notes = s.Notes,
            StartedAtDateTime = s.StartedAtDateTime,
            Title = s.Title
        };
    }

    internal SessionEntity ToEntity(Session s)
    {
        return new SessionEntity
        {
            Id = s.Id,
            EndedAtDateTime = s.EndedAtDateTime,
            Notes = s.Notes,
            StartedAtDateTime = s.StartedAtDateTime,
            Title = s.Title
        };
    }
}