using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class MeetingAttendeesViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public MeetingAttendeesViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(long meetingId)
    {
        var attendees = await _context.MeetingAttendees
            .Include(a => a.Attendee)
            .Where(a => a.MeetingId == meetingId)
            .ToListAsync();

        return View(attendees);
    }
}