namespace Bookings.Web.Domain;

public class Follow : AuditableEntity
{
    public long Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
}