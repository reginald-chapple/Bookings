using System.Security.Claims;
using Bookings.Web.Domain;
using Bookings.Web.Models;

namespace Bookings.Web.Data.Services;

public interface IChatService
{
    Task<ChatViewModel> GetChatModelAsync(Chat chat, ClaimsPrincipal User);
}
