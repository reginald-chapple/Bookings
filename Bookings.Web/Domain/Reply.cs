using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Reply : AuditableEntity
{
    public long Id { get; set; }

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long ConversationId { get; set; }
    public virtual Conversation? Conversation { get; set; }
}