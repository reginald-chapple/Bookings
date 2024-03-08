using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveDonationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long campaignId)
    {
        return View(new Donation
        {
            CampaignId = campaignId
        });
    }
}