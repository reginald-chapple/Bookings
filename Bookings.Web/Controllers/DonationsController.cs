using Microsoft.AspNetCore.Mvc;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using System.Security.Claims;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class DonationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Amount,CampaignId")] Donation donation)
    {
        if (ModelState.IsValid)
        {
            donation.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(donation);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}