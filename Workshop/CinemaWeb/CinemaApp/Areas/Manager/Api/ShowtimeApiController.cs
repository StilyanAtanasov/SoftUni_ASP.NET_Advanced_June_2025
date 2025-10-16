using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.Areas.Manager.Controllers;
using CinemaApp.Web.ViewModels.Manager.ShowtimeSetup;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Api;

[Route("Manager/api/[controller]/[action]")]
[ApiController]
public class ShowtimeApiController : ManagerBaseController
{
    private readonly IShowtimeSetupService _showtimeSetupService;

    public ShowtimeApiController(IShowtimeSetupService showtimeSetupService)
        => _showtimeSetupService = showtimeSetupService;

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateShowtimes(UpdateShowtimesViewModel model)
    {
        if (!ModelState.IsValid) return StatusCode(500);

        ServiceResult sr = await _showtimeSetupService.UpdateShowtimesAsync(model);
        if (sr.IsBadRequest) return StatusCode(400);
        if (!sr.Success) return StatusCode(500);

        return StatusCode(200);
    }
}