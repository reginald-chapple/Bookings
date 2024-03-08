using System.ComponentModel;

namespace Bookings.Web.Domain;

public class Relationship : Entity
{
    public long Id { get; set; }

    public bool IsPrivate { get; set; }

    public RelationshipType Type { get; set; }

    public string SeekerId { get; set; } = string.Empty;
    public virtual ApplicationUser? Seeker { get; set; }

    public string TargetId { get; set; } = string.Empty;
    public virtual ApplicationUser? Target { get; set; }
}

public enum RelationshipType
{
    [Description("Family")]
    Family,
    [Description("Friend")]
    Friend,
    [Description("Acquaintance")]
    Acquaintance,
    [Description("Romantic")]
    Romantic,
    [Description("Professional")]
    Professional
}