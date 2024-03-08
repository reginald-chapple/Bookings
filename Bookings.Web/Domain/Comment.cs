using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Comment : Entity
{
    public long Id { get; set; }

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long TimelineEntryId { get; set; }
    public virtual TimelineEntry? TimelineEntry { get; set; }

    public string AuthorKey { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
}