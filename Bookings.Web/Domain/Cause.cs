namespace Bookings.Web.Domain;

public class Cause : Entity
{
    public long Id { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public long? ParentId { get; set; }
    public virtual Cause? Parent { get; set; }

    public virtual ICollection<Cause> Children { get; set; } = [];
    public virtual ICollection<Campaign> Campaigns { get; set; } = [];
    public virtual ICollection<Community> Communities { get; set; } = [];
}
