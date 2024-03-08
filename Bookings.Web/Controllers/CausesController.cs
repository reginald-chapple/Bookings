using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class CausesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CausesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("{slug}/Campaigns")]
    public async Task<IActionResult> Campaigns(string slug)
    {
        if (slug == null)
        {
            return NotFound();
        }

        var cause = await _context.Causes.Include(c => c.Campaigns).FirstOrDefaultAsync(c => c.Slug == slug);

        if (cause == null)
        {
            return NotFound();
        }

        return View(cause);
    }
}