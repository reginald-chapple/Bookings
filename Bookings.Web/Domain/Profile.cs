namespace Bookings.Web.Domain;

public abstract class Profile : Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
}