using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Admin.UserManagement;

namespace CinemaApp.Services.Core.Admin.Interfaces;

public interface IUserManagementService
{
    Task<ICollection<UserManagementIndexViewModel>> GetAllsersReadonlyAsync();

    Task<ServiceResult> AssignRoleAsync(string userId, string role);

    Task<ServiceResult> RemoveRoleAsync(string userId, string role);
}