namespace Bookings.Web.Models;

public class ChosenAttraction
{
    public long AttractionId { get; set; }
    public string Label { get; set; } = string.Empty;
    public bool Assigned { get; set; } = false;
}