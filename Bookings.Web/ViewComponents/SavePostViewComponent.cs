using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SavePostViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(new Post());
    }
}