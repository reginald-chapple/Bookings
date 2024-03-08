namespace Bookings.Web.Domain
{
    public class Chat : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int MaxUsers { get; set; } = 2;

        public bool IsDisabled { get; set; } = false;
        
        public ChatType Type { get; set; } = ChatType.Private;

        public virtual ICollection<ChatMessage> Messages { get; set; } = [];

        public virtual ICollection<ChatUser> Users { get; set; } = [];

        
    }

    public enum ChatType
    {
        Room,
        Private
    }

    
}
