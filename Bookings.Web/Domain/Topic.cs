using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Topic : AuditableEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Description { get; set; } = string.Empty;

    public long? ParentId { get; set; }
    public virtual Topic? Parent { get; set; }

    public long CommunityId { get; set; }
    public virtual Community? Community { get; set; }

    public virtual ICollection<Topic> Children { get; set; } = [];
    public virtual ICollection<Conversation> Conversations { get; set; } = [];
}