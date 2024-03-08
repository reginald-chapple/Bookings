using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain
{
    public class ChatMessage : Entity
    {
        public long Id { get; set; }

        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty;

        public string AuthorId { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorImage { get; set; } = string.Empty;

        public long ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
