using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class TimelineEntry : Entity
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

    public string AuthorKey { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;

    public virtual ICollection<Comment> Comments { get; set; } = [];
}