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
public class InvitesController : Controller
{
    private readonly ApplicationDbContext _context;

    public InvitesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("{id}/Interview")]
    public async Task<IActionResult> Interview(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invite = await _context.Invites.FirstOrDefaultAsync(i => i.Id == id);

        if (invite == null)
        {
            return NotFound();
        }

        if (invite.InviteeKey != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        if (!invite.HasViewed)
        {
            invite.HasViewed = true;
            _context.Update(invite);
            _context.SaveChanges();
        }

        var model = _context.Meetings
            .Where(m => m.Id == invite.EntityId)
            .Select(m => new InterviewInviteModel
            {
                InviteId = invite.Id,
                Label = m.Name,
                Message = invite.Message,
                Details = m.Details,
                Location = m.Location,
                Date = m.Date,
                StartTime = m.StartTime,
                EndTime = m.EndTime,
                Status = invite.Status,
                ApplicantId = (long) invite.EntityId!,
                CampaignId = (long) m.EntityId!,
                MeetingId = m.Id
            }).FirstOrDefault();

        return View(model);
    }

    [Route("{id}/Interview")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Interview(long? id, [Bind("MeetingId")] AcceptInterviewModel model)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invite = await _context.Invites.FirstOrDefaultAsync(i => i.Id == id);

        if (invite == null)
        {
            return NotFound();
        }

        if (invite.InviteeKey != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        // add user to meeting update invite
        if (ModelState.IsValid)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == model.MeetingId);

            if (meeting == null)
            {
                return NotFound();
            }

            meeting.Attendees.Add(new MeetingAttendee
            {
                AttendeeId = invite.InviteeKey
            });

            meeting.Attendees.Add(new MeetingAttendee
            {
                AttendeeId = invite.CreatedBy
            });

            invite.Status = InviteStatus.Accepted;

            _context.Update(invite);
            _context.Update(meeting);
            _context.SaveChanges();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }

        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}