namespace DevTracker.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllSessions()
    {
        return Ok(); 
    }
}
