using System.ComponentModel;

namespace Bookings.Web.Domain;

public class Relationship : AuditableEntity
{
    public long Id { get; set; }

    public RelationshipType Type { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
}

public enum RelationshipType
{
    [Description("Family")]
    Family,
    [Description("Friend")]
    Friend,
    [Description("Follow")]
    Follow,
    [Description("Professional")]
    Professional
}