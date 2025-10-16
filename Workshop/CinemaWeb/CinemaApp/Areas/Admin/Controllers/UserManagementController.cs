using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.Areas.Administrator.Controllers;
using CinemaApp.Web.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Admin.Controllers;

public class UserManagementController : AdminBaseController
{
    private readonly IUserManagementService _userManagementSerivce;

    public UserManagementController(IUserManagementService userManagementSerivce) => _userManagementSerivce = userManagementSerivce;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ICollection<UserManagementIndexViewModel> model = await _userManagementSerivce.GetAllsersReadonlyAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(string userId, string role)
    {
        ServiceResult sr = await _userManagementSerivce.AssignRoleAsync(userId, role);
        if (!sr.Found) return NotFound();
        if (sr.IsBadRequest) return BadRequest();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveRole(string userId, string role)
    {
        ServiceResult sr = await _userManagementSerivce.RemoveRoleAsync(userId, role);
        if (!sr.Found) return NotFound();
        if (sr.IsBadRequest) return BadRequest();

        return RedirectToAction(nameof(Index));
    }
}