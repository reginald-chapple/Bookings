namespace Bookings.Web.Domain;

public class UserValue : Entity
{
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }

    public long ValueId { get; set; }
    public virtual Value? Value { get; set; }
}