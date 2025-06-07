namespace DevTracker.API.Controllers.v1;

using DevTracker.API.Mappers;
using DevTracker.API.Models;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Core.Domain.Entities;
using DevTracker.Core.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// API controller for managing developer sessions.
/// </summary>
/// <remarks>
/// Sessions can be either coding or theoretical and must be associated with either a goal or a project.
/// </remarks>
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

    /// <summary>
    /// Creates a new session for a goal or a project.
    /// </summary>
    /// <param name="session">The session to create.</param>
    /// <returns>The created session.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GetSessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves all sessions.
    /// </summary>
    /// <returns>A list of all sessions.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<GetSessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReadAllSessions()
    {
        List<SessionEntity> sessions = await _sessionService.ReadAllSessions();
        return Ok(sessions.ConvertAll(_sessionMapper.ToGetSessionDto));
    }

    /// <summary>
    /// Retrieves sessions by type (Theory or Coding).
    /// </summary>
    /// <param name="sessionType">The type of session to filter by (e.g., Theory, Coding).</param>
    /// <returns>A list of sessions matching the type.</returns>
    [HttpGet("by-type")]
    [ProducesResponseType(typeof(IEnumerable<GetSessionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves a session by its ID.
    /// </summary>
    /// <param name="id">The ID of the session.</param>
    /// <returns>The session with the given ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReadSessionById(int id)
    {
        SessionEntity session = await _sessionService.ReadSessionWithId(id);
        return Ok(_sessionMapper.ToGetSessionDto(session));
    }

    /// <summary>
    /// Updates a session by its ID.
    /// </summary>
    /// <param name="id">The ID of the session to update.</param>
    /// <param name="session">The updated session data.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSession(int id, [FromBody] UpdateSessionDto session)
    {
        SessionEntity sessionEntity = await _sessionService.ReadSessionWithId(id);
        sessionEntity = _sessionMapper.ToEntity(sessionEntity, session);
        await _sessionService.UpdateSession(sessionEntity);
        return NoContent();
    }

    /// <summary>
    /// Deletes a session by its ID.
    /// </summary>
    /// <param name="id">The ID of the session to delete.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSession(int id)
    {
        await _sessionService.DeleteSession(id);
        return NoContent();
    }
}
