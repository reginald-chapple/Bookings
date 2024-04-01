namespace Bookings.Web.Models;

public class TeamPositionInputModel
{
    public long TeamId { get; set; }

    public string Position { get; set; } = string.Empty;
}