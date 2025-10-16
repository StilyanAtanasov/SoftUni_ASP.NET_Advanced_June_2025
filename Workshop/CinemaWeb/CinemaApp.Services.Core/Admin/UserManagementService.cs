using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core.Admin;

public class UserManagementService : IUserManagementService
{
    private UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    public UserManagementService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
   
    public async Task<ICollection<UserManagementIndexViewModel>> GetAllsersReadonlyAsync()
    {
        IdentityUser[] users = await _userManager.Users.ToArrayAsync();

        ICollection<UserManagementIndexViewModel> result = new HashSet<UserManagementIndexViewModel>();
        foreach (IdentityUser user in users)
        {
            ICollection<string> roles = (List<string>) await _userManager.GetRolesAsync(user);
            result.Add(new UserManagementIndexViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                Roles = roles
            });
        }

        return result;
    }

    public async Task<ServiceResult> AssignRoleAsync(string userId, string role)
    {
        IdentityUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null) return ServiceResult.NotFound();

        bool roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists) return ServiceResult.BadRequest();

        await _userManager.AddToRoleAsync(user, role);

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> RemoveRoleAsync(string userId, string role)
    {
        IdentityUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null) return ServiceResult.NotFound();

        bool roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists) return ServiceResult.BadRequest();

        await _userManager.RemoveFromRoleAsync(user, role);

        return ServiceResult.Ok();
    }
}