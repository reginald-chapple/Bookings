using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class CalendarController : Controller
{
    private readonly ApplicationDbContext _context;

    public CalendarController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("GetMyMeetings")]
    public async Task<JsonResult> GetMyMeetings()
    {
        if (User == null)
        {
            return new JsonResult(new { Message = "Forbidden" });
        }

        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        if (string.IsNullOrEmpty(claimUserId) || string.IsNullOrWhiteSpace(claimUserId))
        {
            return new JsonResult(new { Message = "Forbidden" });
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == claimUserId);

        if (user == null)
        {
            return new JsonResult(new { Message = "Not found" });
        }

        var meetings = await _context.MeetingAttendees
            .Include(m => m.Meeting)
            .Where(m => m.AttendeeId == user.Id)
            .ToListAsync();

        var events = new List<CalendarModel>();

        foreach (var item in meetings)
        {
            events.Add(new CalendarModel
            {
                Title = item.Meeting!.Name,
                Start = item.Meeting.StartTime,
                End = item.Meeting.EndTime
            });
        }

        return new JsonResult(events);
    }

    [Route("GetCampaignMeetings/{id}")]
    public async Task<JsonResult> GetCampaignMeetings(long? id)
    {
       if (id == null)
       {
            return new JsonResult(new { Message = "Not found" });
       }

       var campaign = await _context.Campaigns.Include(m => m.Meetings)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (campaign == null)
        {
            return new JsonResult(new { Message = "Not found" });
        }

        var events = new List<CalendarModel>();

        foreach (var item in campaign!.Meetings)
        {
            events.Add(new CalendarModel
            {
                Title = item.Name,
                Start = item.StartTime,
                End = item.EndTime
            });
        }

        return new JsonResult(events);
    }
}