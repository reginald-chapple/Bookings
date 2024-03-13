using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Conversation : AuditableEntity
{
    public long Id { get; set; }

    public string Subject { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long TopicId { get; set; }
    public virtual Topic? Topic { get; set; }

    public virtual ICollection<Reply> Replies { get; set; } = [];
}