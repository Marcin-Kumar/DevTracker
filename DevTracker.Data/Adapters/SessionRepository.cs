using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Data.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Entities.Enums;
using DevTracker.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Data.Adapters;
public class SessionRepository : ISessionRepository
{
    private readonly DevTrackerContext _context;
    private readonly SessionDataMapper _sessionMapper;
    public SessionRepository(DevTrackerContext context, SessionDataMapper sessionMapper)
    {
        _context = context;
        _sessionMapper = sessionMapper;
    }

    public async Task<SessionEntity> CreateSession(SessionEntity session)
    {
        Session sessionModel = _sessionMapper.ToModel(session);
        await _context.Sessions.AddAsync(sessionModel);
        await _context.SaveChangesAsync();
        return _sessionMapper.ToEntity(sessionModel);
    }

    public async Task DeleteSession(int id)
    {
        _context.Sessions.Remove(new Session { Id = id, Title = "delete" });
        await _context.SaveChangesAsync();
    }

    public async Task<List<SessionEntity>> ReadAllCodingSessions()
    {
        List<Session> sessionModels = await _context.Sessions.Where(s => s.Type == SessionType.Coding).ToListAsync();
        return sessionModels.ConvertAll(_sessionMapper.ToEntity);
    }

    public async Task<List<SessionEntity>> ReadAllSessions()
    {
        List<Session> sessionModels = await _context.Sessions.ToListAsync();
        return sessionModels.ConvertAll(_sessionMapper.ToEntity);
    }

    public async Task<List<SessionEntity>> ReadAllTheorySessions()
    {
        List<Session> sessionModels = await _context.Sessions.Where(s => s.Type == SessionType.Theory).ToListAsync();
        return sessionModels.ConvertAll(_sessionMapper.ToEntity);
    }

    public async Task<SessionEntity> ReadSessionWithId(int id)
    {
        Session? sessionModel = await _context.Sessions.FindAsync(id);
        if (sessionModel is null)
        {
            throw new KeyNotFoundException($"Session with ID {id} not found.");
        }
        return _sessionMapper.ToEntity(sessionModel);
    }

    public async Task UpdateSession(SessionEntity session)
    {
        Session sessionModel = _sessionMapper.ToModel(session);
        _context.Sessions.Update(sessionModel);
        await _context.SaveChangesAsync();
    }
}
