using CinemaApp.Core.Common.Utils;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.Controllers;
using CinemaApp.Web.ViewModels.User.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class TicketApiController : BaseController
{
    private readonly ITicketService _ticketService;

    public TicketApiController(ITicketService ticketService) => _ticketService = ticketService;

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> BuyTicket([FromBody] BuyTicketViewModel model)
    {
        ServiceResult result = await _ticketService.BuyTicketAsync(GetUserId()!, model);
        if (result.IsBadRequest)
            return BadRequest(result.Errors);

        return Ok();
    }
}