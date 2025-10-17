using CinemaApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Data.Seeding;

public class RoleSeeding
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { UserRoleName, ManagerRoleName, AdminRoleName };

        foreach (string role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
    }

    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string adminEmail = "admin@gmail.com";
        string adminName = adminEmail;
        string adminPass = "123456";

        ApplicationUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            ApplicationUser user = new()
            {
                UserName = adminName,
                Email = adminEmail
            };

            IdentityResult result = await userManager.CreateAsync(user, adminPass);
            if (result.Succeeded) await userManager.AddToRoleAsync(user, AdminRoleName);
            else throw new Exception($"Failed to create admin user: {adminEmail}");
        }
    }
}