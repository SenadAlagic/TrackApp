using TrackApp.Core;

namespace TrackApp.Hubs.Clients;

public interface IChatClient
{
    Task ReceiveMessage(ChatMessage message);
}