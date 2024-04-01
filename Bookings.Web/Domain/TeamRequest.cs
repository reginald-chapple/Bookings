using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class TeamRequest : AuditableEntity
{
    public long Id { get; set; }

    public string Position { get; set; } = string.Empty;

    public bool HasViewed { get; set; } = false;

    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    public long TeamId { get; set; }
    public virtual Team? Team { get; set; }
}

public enum RequestStatus
{
    [Description("Pending")]
    Pending,
    [Description("Review")]
    Review,
    [Description("Withdrawn")]
    Withdrawn,
    [Description("Selected")]
    Selected,
    [Description("Not selected")]
    NotSelected
}