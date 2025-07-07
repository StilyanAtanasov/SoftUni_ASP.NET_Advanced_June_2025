using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaApp.Web.Controllers;

[Authorize]
public class BaseController : Controller
{
	protected bool IsUserAuthenticated()
	{
		if (User.Identity == null) return false;
		return User.Identity.IsAuthenticated;
	}

	protected string? GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
}