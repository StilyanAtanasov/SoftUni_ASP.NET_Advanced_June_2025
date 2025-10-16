using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.ViewModels.Manager.ShowtimeSetup;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Controllers;

public class ShowtimeSetupController : ManagerBaseController
{
    private readonly IShowtimeSetupService _showtimeSetupService;

    public ShowtimeSetupController(IShowtimeSetupService showtimeSetupService)
        => _showtimeSetupService = showtimeSetupService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ICollection<ShowtimeSetupIndexViewModel> model = await _showtimeSetupService.GetAllActiveCinemasReadonlyAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetMoviesWithShowtimes(Guid cinemaId)
    {
        ICollection<MovieShowtimesViewModel> model = await _showtimeSetupService.GetMoviesWithShowtimesReadonlyAsync(cinemaId);

        return Ok(model);
    }
}