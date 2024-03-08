namespace Bookings.Web.Models;

public class CalendarModel
{
    public string Title { get; set; } = "Reserved";

    public DateOnly Start { get; set; }

    public DateOnly End { get; set; }

    public bool AllDay { get; set; } = true;
}
