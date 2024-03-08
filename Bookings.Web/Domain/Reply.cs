using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Reply : Entity
{
    public long Id { get; set; }

    [DataType(DataType.Text)]
    public string Content { get; set; } = string.Empty;

    public long PostId { get; set; }
    public virtual Post? Post { get; set; }

    public string AuthorKey { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
}