namespace Bookings.Web.Models;

public class CalendarModel
{
    public string Title { get; set; } = "Reserved";

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool AllDay { get; set; } = true;
}
