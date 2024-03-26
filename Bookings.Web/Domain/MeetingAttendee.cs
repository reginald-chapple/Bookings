using System.ComponentModel;

namespace Bookings.Web.Domain;

public class MeetingAttendee : Entity
{
    public AttendeeRole Role { get; set; }
    
    public string AttendeeId { get; set; } = string.Empty;
    public virtual ApplicationUser? Attendee { get; set; }

    public long MeetingId { get; set; }
    public virtual Meeting? Meeting { get; set; }
}

public enum AttendeeRole
{
    [Description("Creator")]
    Creator,
    [Description("Staffer")]
    Staffer,
    [Description("Attendee")]
    Attendee
}