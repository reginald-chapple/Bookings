using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Bookings.Web.Administrators.Controllers;

[Authorize(Roles = "Administrator")]
[Area("Administrators")]
[Route("[area]/[controller]")]
public class AttractionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AttractionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Attractions.OrderBy(c => c.Label).ToListAsync());
    }

    [Route("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Label")] Attraction attraction)
    {
        if (ModelState.IsValid)
        {
            _context.Add(attraction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(attraction);
    }
}