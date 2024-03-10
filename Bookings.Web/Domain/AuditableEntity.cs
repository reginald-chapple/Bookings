using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public abstract class AuditableEntity : Entity
{
    [ScaffoldColumn(false)]
    public bool IsDeleted { get; set; } = false;

    [ScaffoldColumn(false)]
    public DateTime? DeleteDate { get; set; }

    [ScaffoldColumn(false)]
    [Editable(false, AllowInitialValue = true)]
    public string CreatedBy { get; set; } = string.Empty;

    [ScaffoldColumn(false)]
    public string UpdatedBy { get; set; } = string.Empty;

    [ScaffoldColumn(false)]
    public string DeletedBy { get; set; } = string.Empty;
}
