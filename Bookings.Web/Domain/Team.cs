using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Team : AuditableEntity
{
    public Team() {}
    
    public long Id { get; set; }

    [DataType(DataType.Text)]
    public string Positions { get; set; } = string.Empty;

    public bool IsOpenForMembership { get; set; } = false;

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<TeamMember> Members { get; set; } = [];
    public virtual ICollection<TeamRequest> Requests { get; set; } = [];
}