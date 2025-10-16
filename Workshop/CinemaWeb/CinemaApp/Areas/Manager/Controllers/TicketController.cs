using CinemaApp.Services.Core.Manager.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Manager.Controllers;

public class TicketController : ManagerBaseController
{
    private readonly IManagerTicketService _ticketService;

    public TicketController(IManagerTicketService ticketService) => _ticketService = ticketService;

    [HttpGet]
    public async Task<IActionResult> Index() => View(await _ticketService.GetAllEligibleCinemasReadonlyAsync());
}