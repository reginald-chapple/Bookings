using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class EventsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Details,Location,StartTime,EndTime,Visibility,CampaignId")] Meeting meeting)
    {
        if (ModelState.IsValid)
        {
            meeting.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            meeting.Attendees.Add(new MeetingAttendee
            {
                AttendeeId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Role = AttendeeRole.Creator
            });
            await _context.AddAsync(meeting);
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

        var meeting = await _context.Meetings.Include(m => m.Campaign).FirstOrDefaultAsync(m => m.Id == id);

        if (meeting == null)
        {
            return NotFound();
        }

        if (meeting.CreatedBy != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(meeting);
    }
}