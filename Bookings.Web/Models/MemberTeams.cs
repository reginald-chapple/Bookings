using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class MemberTeams
{
    public ICollection<TeamMember> Teams { get; set; } = [];

    public ICollection<TeamRequest> Requests { get; set; } = [];
}