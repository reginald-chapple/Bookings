using Microsoft.AspNetCore.SignalR;

namespace Bookings.Web.Hubs
{
    public class NotificationHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;

        // public async Task SendSpecific(string user, string message)
        // {
        //     await Clients.User(user).SendAsync("ReceiveMessage", message);
        // }
    }
}