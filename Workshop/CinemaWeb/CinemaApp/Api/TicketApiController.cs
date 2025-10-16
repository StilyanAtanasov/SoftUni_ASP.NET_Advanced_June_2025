using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.Controllers;
using CinemaApp.Web.ViewModels.User.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class TicketApiController : BaseController
{
    private ITicketService _ticketService;

    public TicketApiController(ITicketService ticketService) => _ticketService = ticketService;

    [HttpPost]
    public async Task<IActionResult> ButTicket([FromBody] BuyTicketViewModel model)
    {

    }
}