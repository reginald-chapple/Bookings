using Bookings.Web.Data;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class DatingSearchViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginatedList<MatchProfile> profiles)
    {
        return View(profiles);
    }
}