using Microsoft.AspNetCore.SignalR;

namespace _01._Chat.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessageAsync(string message, string user)
        => await Clients.All.SendAsync("ReceiveMessage", user, message);
}
