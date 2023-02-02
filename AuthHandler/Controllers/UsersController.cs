using System.Linq.Expressions;
using AuthHandler.Filters;
using AuthHandler.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthHandler.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersStore _usersStore;

    public UsersController(UsersStore usersStore)
    {
        _usersStore = usersStore;
    }

    [HttpGet("user")]
    [Role("userrole")]
    public IActionResult GetUserMe()
    {
        Claim? email = User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        return Ok("UserEmail: " + email?.Value);
    }

    [HttpGet]
    [Role("userrole,admin")]
    public IActionResult GetMe()
    {
        Claim? email = User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        return Ok("email: " + email?.Value);
    }

    [HttpGet("admin")]
    [Role("admin")]
    public IActionResult GetAdminMe()
    {
        Claim? username = User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        Claim? email = User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        return Ok("Userphone: " + username?.Value + ", email: " + email?.Value);
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
        var key = Guid.NewGuid().ToString();

        _usersStore.Users.Add(key, user);
        return Ok(key);
    }
}