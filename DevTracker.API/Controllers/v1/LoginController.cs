using DevTracker.API.Dtos;
using DevTracker.Core.Application.InboundPorts;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers.v1;

/// <summary>
/// API Controller to handle user login
/// </summary>
/// <remarks>
/// Validates and logs in the user
/// </remarks>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    protected LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    /// <summary>
    /// Authenticates user.
    /// </summary>
    /// <remarks>This method validates the provided username and password. </remarks>
    /// <param name="loginDto">The login credentials, including the username and password, provided in the request body.</param>
    /// <returns>A token string if authentication is successful, or an unauthorized
    /// response if the credentials are invalid.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] PostLoginDto loginDto)
    {
        if (_loginService.ValidateLogin(loginDto.username, loginDto.password))
        {
            string token = _loginService.GenerateToken(loginDto.username);
            return Ok(token);
        }
        
        return Unauthorized("Invalid username or password.");
    }
}
