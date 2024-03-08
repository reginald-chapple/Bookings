using Microsoft.AspNetCore.SignalR;

namespace Bookings.Web.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}
