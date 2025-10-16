using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.Areas.Manager.Controllers;
using CinemaApp.Web.ViewModels.Manager.TicketManagement;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Api;

[Route("Manager/api/[controller]/[action]")]
[ApiController]
public class TicketApiController : ManagerBaseController
{
    private readonly IManagerTicketService _ticketService;

    public TicketApiController(IManagerTicketService ticketService)
        => _ticketService = ticketService;

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetMoviesByCinema(Guid cinemaId)
    {
        if (!ModelState.IsValid) return StatusCode(500);

        ICollection<CinemaMovieTicketsCountViewModel> model = 
            await _ticketService.GetCinemaMoviesTicketsCountReadonlyAsync(cinemaId);
       
        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateAvailableTickets(UpdateCinemaMovieTicketsCountViewModel model)
    {
        if (!ModelState.IsValid) return StatusCode(500);

        ServiceResult sr = await _ticketService.UpdateCinemaMoviesTicketsAsync(model);
        if (sr.IsBadRequest) return StatusCode(400);

        return Ok();
    }
}
