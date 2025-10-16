using System.Security.Claims;
using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Web.Middlewares;

public class ManagerAccessMiddleware
{
    private readonly RequestDelegate _next;

    public ManagerAccessMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path.ToString().ToLower();

        if (path.StartsWith("/manager"))
        {
            ClaimsPrincipal user = context.User;

            if (!user.Identity?.IsAuthenticated ?? true || !user.IsInRole(ManagerRoleName))
            {
                context.Response.Redirect("/Home/AccessDenied");
                return;
            }
        }

        await _next(context);
    }
}
