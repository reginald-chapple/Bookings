using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Invite : AuditableEntity
{
    public Invite() {}

    public long Id { get; set; }

    public string Label { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Message { get; set; } = string.Empty;

    public InviteStatus Status { get; set; } = InviteStatus.Pending;

    public InviteType Type { get; set; }

    public bool HasViewed { get; set; } = false;

    public string InviteeKey { get; set; } = string.Empty;

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
    [Description("Task")]
    Task,
    [Description("Chat")]
    Chat,
}