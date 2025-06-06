using DevTracker.API.Dtos;
using DevTracker.Core.Application.InboundPorts;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Controllers.v1;

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

    [HttpPost]
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
