using Bookings.Web.Data;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class SaveCampaignViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public SaveCampaignViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        ViewData["CauseId"] = _context.Causes.Include(c => c.Children).ToList();
        return View(new Campaign());
    }
}