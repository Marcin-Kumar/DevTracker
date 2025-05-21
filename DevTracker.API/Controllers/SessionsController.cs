namespace DevTracker.API.Controllers;

using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Entities.Enums;
using DevTracker.Domain.InboundPorts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly SessionDtoMapper _sessionMapper;

    public SessionsController(ISessionService sessionService, SessionDtoMapper sessionMapper)
    {
        _sessionService = sessionService;
        _sessionMapper = sessionMapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession([FromBody] CreateSessionDto session)
    {
        SessionEntity sessionEntity = _sessionMapper.ToEntity(session);

        if ((session.GoalId == null && session.ProjectId == null) || (session.GoalId != null && session.ProjectId != null))
        {
            return BadRequest("Provide the correct GoalId or ProjectId.");
        }
        if (session.GoalId != null)
        {
            sessionEntity = await _sessionService.CreateSessionForGoalWithId((int)session.GoalId, sessionEntity);
        }
        else if (session.ProjectId != null)
        {
            sessionEntity = await _sessionService.CreateSessionForProjectWithId((int)session.ProjectId, sessionEntity);
        }

        GetSessionDto responseDto = _sessionMapper.ToGetSessionDto(sessionEntity);
        return CreatedAtAction(nameof(ReadSessionById), new { id = responseDto.Id }, responseDto);
    }

    [HttpGet]
    public async Task<IActionResult> ReadAllSessions()
    {
        List<SessionEntity> sessions = await _sessionService.ReadAllSessions();
        return Ok(sessions.Select(_sessionMapper.ToGetSessionDto).ToList()); 
    }

    [HttpGet("by-type")]
    public async Task<IActionResult> ReadSessionsByType([FromQuery] string sessionType)
    {
        List<SessionEntity> sessions;
        SessionType type;
        Enum.TryParse<SessionType>(sessionType, ignoreCase: true, out type);
        if (SessionType.Theory.Equals(type))
        {
            sessions = await _sessionService.ReadAllTheorySessions();
        } else if (SessionType.Coding.Equals(type))
        {
            sessions = await _sessionService.ReadAllCodingSessions();
        }
        else
        {
            return BadRequest("Invalid session type.");
        }
        return Ok(sessions.Select(_sessionMapper.ToGetSessionDto).ToList()); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadSessionById(int id)
    {
        SessionEntity session = await _sessionService.ReadSessionWithId(id);
        return Ok(_sessionMapper.ToGetSessionDto(session));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSession(int id, [FromBody] UpdateSessionDto session)
    { 
        SessionEntity sessionEntity = await _sessionService.ReadSessionWithId(id);
        sessionEntity = _sessionMapper.ToEntity(sessionEntity, session);
        await _sessionService.UpdateSession(sessionEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(int id)
    {
        await _sessionService.DeleteSession(id);
        return NoContent();
    }
}
