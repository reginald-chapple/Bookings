using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveActionItemViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long parentId, long campaignId)
    {
        return View(new ActionItem
        {
            ParentId = parentId,
            CampaignId = campaignId
        });
    }
}