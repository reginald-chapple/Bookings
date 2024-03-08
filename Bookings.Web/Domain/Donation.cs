using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Domain;

public class Donation : Entity
{
    public Donation() {}
    
    public long Id { get; set; }

    [Precision(8, 2)]
    public decimal Amount { get; set; } = 0.0m;

    public string DonorEmail { get; set; } = string.Empty;
    public string DonorName { get; set; } = string.Empty;

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

}