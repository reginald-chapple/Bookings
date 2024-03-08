using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Invite : Entity
{
    public long Id { get; set; }

    public string Label { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Message { get; set; } = string.Empty;

    public InviteStatus Status { get; set; } = InviteStatus.Pending;

    public InviteType Type { get; set; }

    public bool HasViewed { get; set; } = false; // only invitee can view anyone else will have there account locked. If the cuurent user doesn't match the invitee key.

    public bool WasDeleted { get; set; } = false;

    public DateTime? DeleteDate { get; set; }

    public string InviteeKey { get; set; } = string.Empty;
    public string InviteeName { get; set; } = string.Empty;

    public string InviterKey { get; set; } = string.Empty;
    public string InviterName { get; set; } = string.Empty;

    public long? EntityId { get; set; }
    public EntityType EntityType { get; set; } = EntityType.None;
}

public enum InviteStatus
{
    [Description("Pending")]
    Pending,
    [Description("Accepted")]
    Accepted,
    [Description("Declined")]
    Declined
}

public enum InviteType
{
    [Description("Event")]
    Event,
    [Description("Opportunity")]
    Opportunity,
    [Description("Interview")]
    Interview,
    [Description("Task")]
    Task,
    [Description("Chat")]
    Chat,
    [Description("Date")]
    Date
}