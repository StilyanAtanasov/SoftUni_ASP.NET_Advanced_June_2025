using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaApp.Data.Seeding;

public class RoleSeeding
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "User", "Manager" };

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        string managerEmail = "manager@gmail.com";
        string managerName = "manager@gmail.com";

        var managerUser = await userManager.FindByEmailAsync(managerEmail);

        if (managerUser == null)
        {
            IdentityUser user = new ()
            {
                UserName = managerName,
                Email = managerEmail
            };

            IdentityResult result = await userManager.CreateAsync(user, "123456"); 

            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Manager");
        }
    } 
}