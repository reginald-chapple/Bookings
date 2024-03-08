namespace Bookings.Web.Domain;

public class Attraction : Entity
{
    public long Id { get; set; }

    public string Label { get; set; } = string.Empty;
}