using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Bookings.Web.Data.Repository;

namespace Bookings.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chat;
        private readonly IChatRepository _repo;

        public ChatController(IHubContext<ChatHub> chat, IChatRepository repo)
        {
            _chat = chat;
            _repo = repo;
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinChat(string connectionId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveChat(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string content, string roomName, int chatId, [FromServices]ApplicationDbContext context)
        {
            var message = new ChatMessage
            {
                ChatId = chatId,
                Content = content,
                AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                AuthorName = User.FindFirst("FullName")!.Value,
                AuthorImage = User.FindFirst("Image")!.Value
            };

            context.ChatMessages.Add(message);
            await context.SaveChangesAsync();

            await _chat.Clients.Group(roomName)
                .SendAsync("ReceiveMessage", _repo.GetMessageById(message.Id));

            return Ok();
        }
    }
}
