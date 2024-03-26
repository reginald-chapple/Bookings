using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Meeting : AuditableEntity
{
    public long Id { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Details { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public MeetingVisibility Visibility { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime EndTime { get; set; }

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<MeetingAttendee> Attendees { get; set; } = [];
}

public enum MeetingVisibility
{
    [Description("Private")]
    Private,
    [Description("Team")]
    Team,
    [Description("Public")]
    Public
}