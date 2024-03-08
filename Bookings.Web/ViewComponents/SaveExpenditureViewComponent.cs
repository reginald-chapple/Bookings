using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveExpenditureViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long campaignId)
    {
        return View(new Expenditure
        {
            CampaignId = campaignId
        });
    }
}