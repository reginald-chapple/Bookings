using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveMilestoneViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long campaignId)
    {
        return View(new ActionItem
        {
            CampaignId = campaignId
        });
    }
}