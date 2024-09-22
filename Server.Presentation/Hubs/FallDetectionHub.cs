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
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            await Clients.All.SendAsync("UserDisconnected");
            await base.OnDisconnectedAsync(ex);
        }
    }
}
