namespace Bookings.Web.Domain
{
    public class UserNotification : Entity
    {
        public long NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public bool IsRead { get; set; } = false;
    }
}