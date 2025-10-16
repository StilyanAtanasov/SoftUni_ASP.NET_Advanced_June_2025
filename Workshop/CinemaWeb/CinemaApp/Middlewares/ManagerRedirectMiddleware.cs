using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Web.Middlewares;

public class ManagerRedirectMiddleware
{
    private const string IndexPath = "/";
    private const string ManagerIndexPath = "/Manager/Home/Index";
    private readonly RequestDelegate _next;

    public ManagerRedirectMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.IsInRole(ManagerRoleName) && context.Request.Path == IndexPath)
        {
            context.Response.Redirect(ManagerIndexPath);
            return;
        }

        await _next(context);
    }
}
