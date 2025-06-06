namespace DevTracker.API.Controllers.v1;

using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Core.Domain.Entities;
using DevTracker.Core.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]

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

        if (session.GoalId is null && session.ProjectId is null || session.GoalId is not null && session.ProjectId is not null)
        {
            return BadRequest("Provide the correct GoalId or ProjectId.");
        }
        if (session.GoalId is not null)
        {
            sessionEntity = await _sessionService.CreateSessionForGoalWithId((int)session.GoalId, sessionEntity);
        }
        else if (session.ProjectId is not null)
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
        return Ok(sessions.ConvertAll(_sessionMapper.ToGetSessionDto));
    }

    [HttpGet("by-type")]
    public async Task<IActionResult> ReadSessionsByType([FromQuery] string sessionType)
    {
        List<SessionEntity> sessions;
        Enum.TryParse(sessionType, ignoreCase: true, out SessionType type);
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
        return Ok(sessions.ConvertAll(_sessionMapper.ToGetSessionDto));
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
