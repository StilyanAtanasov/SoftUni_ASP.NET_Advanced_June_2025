using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Web.Middlewares;

public class AdminRedirectMiddleware
{
    private const string IndexPath = "/";
    private const string AdminIndexPath = "/Admin/Home/Index";
    private readonly RequestDelegate _next;

    public AdminRedirectMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.IsInRole(AdminRoleName) && context.Request.Path == IndexPath)
        {
            context.Response.Redirect(AdminIndexPath);
            return;
        }

        await _next(context);
    }
}
