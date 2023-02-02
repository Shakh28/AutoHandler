using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthHandler.Controllers;

[Route("admins")]
[ApiController]
public class AdminsController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetAdmin()
    {
        var scheme = User;
        return Ok("admins list");
    }
}