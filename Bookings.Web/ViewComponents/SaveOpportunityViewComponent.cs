using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveOpportunityViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long campaignId)
    {
        return View(new Opportunity
        {
            CampaignId = campaignId
        });
    }
}