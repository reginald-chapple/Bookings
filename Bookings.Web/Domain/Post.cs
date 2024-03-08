using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Post : Entity
{
    public long Id { get; set; }

    public string Subject { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long ForumId { get; set; }
    public virtual Forum? Forum { get; set; }

    public string AuthorKey { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;

    public virtual ICollection<Reply> Replies { get; set; } = [];
}