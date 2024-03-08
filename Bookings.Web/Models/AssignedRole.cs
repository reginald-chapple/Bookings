namespace Bookings.Web.Models;

public class AssignedRole
{
    public string RoleId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Assigned { get; set; } = false;
}
