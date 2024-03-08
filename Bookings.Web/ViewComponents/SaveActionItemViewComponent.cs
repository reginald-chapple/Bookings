using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveActionItemViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long milestoneId)
    {
        return View(new ActionItem
        {
            MilestoneId = milestoneId
        });
    }
}