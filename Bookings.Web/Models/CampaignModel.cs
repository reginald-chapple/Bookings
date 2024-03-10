using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class CampaignDetailsModel
{
    public virtual CreatorModel? Creator { get; set; }

    public virtual Campaign? Campaign { get; set; }
}

public class CreatorCampaignsModel
{
    public virtual CreatorModel? Creator { get; set; }

    public virtual ICollection<Campaign> Campaigns { get; set; } = [];
}