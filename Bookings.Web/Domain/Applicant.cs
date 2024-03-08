using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Applicant : Entity
{
    public long Id { get; set; }

    public string ResumeId { get; set; } = string.Empty;
    public virtual Resume? Resume { get; set; }

    public bool HasViewed { get; set; } = false;

    public ApplicantStatus Status { get; set; } = ApplicantStatus.Pending;

    public long OpportunityId { get; set; }
    public virtual Opportunity? Opportunity { get; set; }
}

public enum ApplicantStatus
{
    [Description("Pending")]
    Pending,
    [Description("Review")]
    Review,
    [Description("Interview")]
    Interview,
    [Description("Withdrawn")]
    Withdrawn,
    [Description("Selected")]
    Selected,
    [Description("Not selected")]
    NotSelected
}