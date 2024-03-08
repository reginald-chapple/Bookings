namespace Bookings.Web.Domain;

public class Volunteer : Entity
{
    public long OpportunityId { get; set; }
    public virtual Opportunity? Opportunity { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
}
