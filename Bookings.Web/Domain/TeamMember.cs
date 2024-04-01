namespace Bookings.Web.Domain;

public class TeamMember : Entity
{
    public string Position { get; set; } = string.Empty;

    public string MemberId { get; set; } = string.Empty;
    public virtual ApplicationUser? Member { get; set; }

    public long TeamId { get; set; }
    public virtual Team? Team { get; set; }
}