using _02._Coffee.Hubs;
using _02._Coffee.Models;
using _02._Coffee.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace _02._Coffee.Controllers;

public class CoffeeController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IHubContext<CoffeeHub> _coffeeHub;

    public CoffeeController(IOrderService orderService, IHubContext<CoffeeHub> coffeeHub)
    {
        _orderService = orderService;
        _coffeeHub = coffeeHub;
    }

    [HttpPost]
    public async Task<IActionResult> OrderCoffee([FromBody] Order order)
    {
        await _coffeeHub.Clients.All.SendAsync("NewOrder", order);
        int orderId = _orderService.NewOrder();
        return Accepted(orderId);
    }
}
