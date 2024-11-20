using Microsoft.AspNetCore.SignalR;

namespace Server.Presentation.Hubs
{
    public class FailDetectionHub : Hub
    {

        public FailDetectionHub()
        {

        }

        public Task SendMessageToAll(string message)
        {
            return Clients.Others.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserConnected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            await Clients.Others.SendAsync("UserDisconnected");
            await base.OnDisconnectedAsync(ex);
        }
    }
}
