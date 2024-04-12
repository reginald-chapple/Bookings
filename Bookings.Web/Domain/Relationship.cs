using System.ComponentModel;

namespace Bookings.Web.Domain;

public class Relationship : AuditableEntity
{
    public long Id { get; set; }

    public RelationshipType Type { get; set; }

    public RelationshipStatus Status { get; set; } = RelationshipStatus.Pending;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
}

public enum RelationshipType
{
    [Description("Social")]
    Social,
    [Description("Follow")]
    Follow,
    [Description("Professional")]
    Professional
}

public enum RelationshipStatus
{
    [Description("Pending")]
    Pending,
    [Description("Accepted")]
    Accepted,
    [Description("Declined")]
    Declined,
    [Description("Removed")]
    Removed,
    [Description("Blocked")]
    Blocked
}