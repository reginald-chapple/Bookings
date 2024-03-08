using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Resume : Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [DataType(DataType.Text)]
    public string Skills { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Experience { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Education { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Background { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<Applicant> Applications { get; set; } = [];
}