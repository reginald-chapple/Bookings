namespace Bookings.Web.Domain;

public class SkinComplexion : Entity
{
    public int Id { get; set; }

    public string ScalePosition { get; set; } = string.Empty;

    public string HexCode { get; set; } = string.Empty;

    public ICollection<MatchProfile> Profiles { get; set; } = [];
}