namespace Bookings.Web.Models;

public class MeetingAttendeesModel
{
    public long MeetingId { get; set; }
    public List<CreatorModel> Attendees { get; set; } = [];
}