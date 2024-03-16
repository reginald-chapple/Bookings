using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Post : AuditableEntity
{
    public Post() {}
    
    public long Id { get; set; }

    public string Subject { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long? CommunityId { get; set; }
    public virtual Community? Community { get; set; }

    public virtual ICollection<Reply> Replies { get; set; } = [];
}