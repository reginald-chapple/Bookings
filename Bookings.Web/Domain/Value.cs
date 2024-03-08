using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Value : Entity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<UserValue> Users { get; set; } = [];
}