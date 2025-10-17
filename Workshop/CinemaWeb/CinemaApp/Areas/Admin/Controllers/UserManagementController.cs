using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.Areas.Administrator.Controllers;
using CinemaApp.Web.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Admin.Controllers;

public class UserManagementController : AdminBaseController
{
    private readonly IUserManagementService _userManagementService;

    public UserManagementController(IUserManagementService userManagementService) => _userManagementService = userManagementService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ICollection<UserManagementIndexViewModel> model = await _userManagementService.GetAllUsersReadonlyAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(string userId, string role)
    {
        ServiceResult sr = await _userManagementService.AssignRoleAsync(userId, role);
        if (!sr.Found) return NotFound();
        if (sr.IsBadRequest) return BadRequest();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveRole(string userId, string role)
    {
        ServiceResult sr = await _userManagementService.RemoveRoleAsync(userId, role);
        if (!sr.Found) return NotFound();
        if (sr.IsBadRequest) return BadRequest();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        ServiceResult sr = await _userManagementService.DeleteUserAsync(userId);
        if (!sr.Found) return NotFound();

        return RedirectToAction(nameof(Index));
    }
}