using System.ComponentModel.DataAnnotations;
using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class InterviewInviteModel
{
    public string Label { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Message { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Details { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    [DataType(DataType.Time)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.Time)]
    public DateTime EndTime { get; set; }

    public InviteStatus Status { get; set; } = InviteStatus.Pending;

    public long InviteId { get; set; } = 0;

    public long CampaignId { get; set; }

    public long ApplicantId { get; set; }

    public long MeetingId { get; set; } = 0;
}