using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static CinemaApp.GCommon.ApplicationConstants.Roles;

namespace CinemaApp.Data.Seeding;

public class RoleSeeding
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { UserRoleName, ManagerRoleName, AdminRoleName };

        foreach (string role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        string managerEmail = "manager@gmail.com";
        string managerName = managerEmail;

        string adminEmail = "admin@gmail.com";
        string adminName = adminEmail;

        IdentityUser? managerUser = await userManager.FindByEmailAsync(managerEmail);
        IdentityUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (managerUser == null)
        {
            IdentityUser user = new ()
            {
                UserName = managerName,
                Email = managerEmail
            };

            IdentityResult result = await userManager.CreateAsync(user, "123456");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, ManagerRoleName);
        }

        if (adminUser == null)
        {
            IdentityUser user = new()
            {
                UserName = adminName,
                Email = adminEmail
            };

            IdentityResult result = await userManager.CreateAsync(user, "123456");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, AdminRoleName);
        }
    } 
}