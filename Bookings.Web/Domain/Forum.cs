using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Forum : Entity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Description { get; set; } = string.Empty;

    public string ModeratorKey { get; set; } = string.Empty;
    public string ModeratorName { get; set; } = string.Empty;
    public string ModeratorImage { get; set; } = string.Empty;

    public long? ParentId { get; set; }
    public virtual Forum? Parent { get; set; }

    public virtual ICollection<Forum> Children { get; set; } = [];
    public virtual ICollection<Post> Posts { get; set; } = [];
}