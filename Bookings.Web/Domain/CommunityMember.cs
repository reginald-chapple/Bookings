using System.ComponentModel;

namespace Bookings.Web.Domain;

public class CommunityMember : Entity
{
    public CommunityRole Role { get; set; } = CommunityRole.Member;
    
    public long CommunityId { get; set; }
    public virtual Community? Community { get; set; }

    public string MemberId { get; set; } = string.Empty;
    public virtual ApplicationUser? Member { get; set; }
}

public enum CommunityRole
{
    [Description("Member")]
    Member,
    [Description("Moderator")]
    Moderator
}