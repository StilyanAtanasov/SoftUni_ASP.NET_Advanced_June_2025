using CinemaApp.Services.Core.Manager.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Controllers;

public class ConfigureCinemasController : ManagerBaseController
{
    private readonly ICinemaConfigurationService _cinemaConfigurationService;

    public ConfigureCinemasController(ICinemaConfigurationService cinemaConfigurationService)
        => _cinemaConfigurationService = cinemaConfigurationService;
    
    public async Task<IActionResult> Index()
        => View(await _cinemaConfigurationService.GetAllCinemasReadonlyAsync());
}