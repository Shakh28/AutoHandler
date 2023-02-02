using AuthHandler.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace AuthHandler.Filters;

public class AuthAttribute : IActionFilter
{
    private readonly UsersStore _usersStore;
    public string Roles { get; set; }

    public AuthAttribute(UsersStore usersStore, string roles)
    {
        _usersStore = usersStore;
        Roles = roles;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authorization = context.HttpContext.Request.Headers[HeaderNames.Authorization];

        if (!_usersStore.Users.ContainsKey(authorization))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = _usersStore.Users[authorization];

        if (!Roles.Contains(user.Role!))
        {
            context.Result = new JsonResult(new { Error = "Access denied" });
            return;
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Role!),
            new Claim(ClaimTypes.Name, user.Name!)
        };

        var identity = new ClaimsIdentity(claims);

        var principal = new ClaimsPrincipal(identity);

        context.HttpContext.User = principal;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}