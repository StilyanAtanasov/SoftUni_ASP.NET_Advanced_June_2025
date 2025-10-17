using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Data.Seeding;
using CinemaApp.Services.Core;
using CinemaApp.Services.Core.Admin;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Services.Core.Manager;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<CinemaAppDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CinemaAppDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
        builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
        builder.Services.AddScoped<ICinemaMovieRepository, CinemaMovieRepository>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();

        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<IWatchlistService, WatchlistService>();
        builder.Services.AddScoped<ICinemaService, CinemaService>();
        builder.Services.AddScoped<ITicketService, TicketService>();

        builder.Services.AddScoped<ICinemaManagementService, CinemaManagementService>();
        builder.Services.AddScoped<IMovieManagementService, MovieManagementService>();
        builder.Services.AddScoped<IUserManagementService, UserManagementService>();

        builder.Services.AddScoped<ICinemaConfigurationService, CinemaConfigurationService>();
        builder.Services.AddScoped<IProgramSetupService, ProgramSetupService>();
        builder.Services.AddScoped<IShowtimeSetupService, ShowtimeSetupService>();
        builder.Services.AddScoped<IManagerTicketService, ManagerTicketService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseMiddleware<ManagerAccessMiddleware>();

        app.UseAuthorization();

        app.UseMiddleware<AdminRedirectMiddleware>();
        app.UseMiddleware<ManagerRedirectMiddleware>();

        app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        await RoleSeeding.SeedRolesAsync(serviceProvider);
        await RoleSeeding.SeedAdminAsync(serviceProvider);

        app.Run();
    }
}