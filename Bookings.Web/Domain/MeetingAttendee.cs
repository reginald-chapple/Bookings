namespace Bookings.Web.Domain;

public class MeetingAttendee : Entity
{
    public string AttendeeId { get; set; } = string.Empty;
    public virtual ApplicationUser? Attendee { get; set; }

    public long MeetingId { get; set; }
    public virtual Meeting? Meeting { get; set; }
}