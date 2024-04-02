using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class MilestonesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MilestonesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Label,Details,CampaignId")] ActionItem milestone)
    {
        if (ModelState.IsValid)
        {
            milestone.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            milestone.Type = ActionItemType.Milestone;
            await _context.AddAsync(milestone);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{id}/Details")]
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var milestone = await _context.ActionItems
            .Include(m => m.Children)
            .Include(m => m.Campaign)
                .ThenInclude(c => c!.Cause)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (milestone == null)
        {
            return NotFound();
        }

        if (milestone.CreatedBy != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        return View(milestone);
    }
}