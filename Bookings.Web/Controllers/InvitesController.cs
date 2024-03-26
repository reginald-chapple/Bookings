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

    public async Task<IActionResult> Send([Bind("Id,Label,Message,EntityId")] Invite invite, string[] selectedUsers)
    {
        if (ModelState.IsValid)
        {
            var newInvites = new List<Invite>();
            
            if (selectedUsers != null)
            {
                foreach (var user in selectedUsers)
                {
                    if (!_context.Invites.Where(i => i.InviteeKey == user).Any())
                    {
                        newInvites.Add(new Invite
                        {
                            Label = invite.Label,
                            Message = invite.Message,
                            Type = InviteType.Event,
                            EntityType = EntityType.Meeting,
                            EntityId = invite.EntityId,
                            InviteeKey = user,
                            CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                        });
                    }
                }
            }
            await _context.AddRangeAsync(newInvites);
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
                StartTime = m.StartTime,
                EndTime = m.EndTime,
                Status = invite.Status,
                ApplicantId = (long) invite.EntityId!,
                CampaignId = m.CampaignId,
                MeetingId = m.Id
            }).FirstOrDefault();

        var  meetingAttendees = _context.MeetingAttendees.Where(a => a.MeetingId == model!.MeetingId).Select(m => m.AttendeeId).ToList();

        ViewData["IsAttending"] = meetingAttendees.Contains(invite.InviteeKey);

        return View(model);
    }

    [Route("{id}/Accept")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(long? id, [Bind("MeetingId")] AcceptInviteModel model)
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

            invite.Status = InviteStatus.Accepted;

            _context.Update(invite);
            _context.Update(meeting);
            _context.SaveChanges();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }

        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}