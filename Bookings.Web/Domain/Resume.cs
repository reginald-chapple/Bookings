using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Resume : AuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Applicant { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Skills { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Experience { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Education { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Background { get; set; } = string.Empty;

    public virtual ICollection<Applicant> Applications { get; set; } = [];
}