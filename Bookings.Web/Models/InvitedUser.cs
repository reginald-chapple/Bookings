namespace Bookings.Web.Models;

public class InvitedUser
{
    public string InviteeKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Assigned { get; set; } = false;
}
