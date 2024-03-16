using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Reply : AuditableEntity
{
    public long Id { get; set; }

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long PostId { get; set; }
    public virtual Post? Post { get; set; }

    public long? ParentId { get; set; }
    public virtual Reply? Parent { get; set; }

    public virtual ICollection<Reply> Children { get; set; } = [];
}