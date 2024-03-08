using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Domain;

public class Expenditure : Entity
{
    public Expenditure() {}
    
    public long Id { get; set; }

    public string Item { get; set; } = string.Empty;

    [Precision(8, 2)]
    public decimal Cost { get; set; }

    public bool IsAcquired { get; set; } = false;

    public DateTime? AcquisitionDate { get; set; }

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

}