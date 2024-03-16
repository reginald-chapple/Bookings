using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Community : AuditableEntity
{
    public Community() { }
    
    public long Id { get; set; }

    public bool IsEnabled { get; set; } = false;

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

    public ICollection<CommunityMember> Members { get; set; } = [];
    public ICollection<Post> Posts { get; set; } = [];
}