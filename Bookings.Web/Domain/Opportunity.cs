using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Opportunity : AuditableEntity
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Details { get; set; } = string.Empty;

    public bool IsClosed { get; set; } = false;

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<Volunteer> Volunteers { get; set; } = [];
    public virtual ICollection<Applicant> Applicants { get; set; } = [];

}