using _02._Coffee.Models;
using _02._Coffee.Services.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace _02._Coffee.Hubs;

public class CoffeeHub : Hub
{
    private readonly IOrderService _orderService;

    public CoffeeHub(IOrderService orderService) => _orderService = orderService;

    public async Task GetUpdateForOrderAsync(int orderId)
    {
        CheckResult result;

        do
        {
            result = _orderService.GetUpdate(orderId);
            if (result.New) await Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
        }
        while(!result.Finished);

        await Clients.Caller.SendAsync("Finished");
    }
}