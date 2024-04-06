using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class CausesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICampaignService _campaignService;

    public CausesController(ApplicationDbContext context, ICampaignService campaignService)
    {
        _context = context;
        _campaignService = campaignService;
    }

    [Route("{slug}/Campaigns")]
    public async Task<IActionResult> Campaigns(string slug)
    {
        if (slug == null)
        {
            return NotFound();
        }

        var cause = await _context.Causes
            .Include(c => c.Children)
            .Include(c => c.Campaigns)
            .FirstOrDefaultAsync(c => c.Slug == slug);

        if (cause == null)
        {
            return NotFound();
        }
        ViewData["CurrentFilter"] = string.Empty;
        ViewData["Causes"] = _context.Causes.Where(c => c.ParentId == null).OrderBy(c => c.Name).ToList();
        return View(cause);
    }

    
}