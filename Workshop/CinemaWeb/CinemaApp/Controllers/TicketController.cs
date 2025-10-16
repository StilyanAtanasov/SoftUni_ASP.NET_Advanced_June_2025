using CinemaApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers;

public class TicketController : BaseController
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService) => _ticketService = ticketService;

    [HttpGet]
    public async Task<IActionResult> Index()
        => View(await  _ticketService.GetUserTicketsReadonlyAsync(GetUserId()!));
}