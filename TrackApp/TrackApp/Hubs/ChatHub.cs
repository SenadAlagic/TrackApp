using Microsoft.AspNetCore.SignalR;
using TrackApp.Core;
using TrackApp.Hubs.Clients;

namespace TrackApp.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(ChatMessage message)
    {
        await Clients.All.ReceiveMessage(message);
    }
}