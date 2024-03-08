using Bookings.Web.Domain;
using Bookings.Web.Models;

namespace Bookings.Web.Data.Repository
{
    public interface IChatRepository
    {
        ChatMessageResponseModel GetMessageById(long messageId);
        Task<Chat> GetChatByNameAsync(string name);
        Task CreateBusinessChatAsync(string name, string currentUserId, string otherUserId);
        bool ChatExists(string name);
        Task CreatePrivateChatAsync(string currentUserId, string otherUserId);
        Task RemovePrivateChatAsync(string currentUserId, string otherUserId);
    }
}