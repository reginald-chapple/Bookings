using Bookings.Web.Domain;

namespace Bookings.Web.Models
{
    public class ChatMessageModel
    {
        public ChatMessage? Message { get; set; }
        public UserModel? Author { get; set; }
    }
}