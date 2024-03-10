using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Bookings.Web.Infrastructure.Helpers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class ApplicantsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ApplicantsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("{id}/Details")]
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var applicant = await _context.Applicants
            .Include(a => a.Opportunity)
            .Include(a => a.Resume)
                .ThenInclude(a => a!.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (applicant == null)
        {
            return NotFound();
        }

        if (!applicant.HasViewed)
        {
            applicant.HasViewed = true;
            applicant.Status = ApplicantStatus.Review;
            _context.Update(applicant);
            _context.SaveChanges();
        }

        return View(applicant);
    }

    [Route("{id}/Interview-Invite")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InterviewInvite(long? id, [Bind("Label,Message,Details,Location,Date,StartTime,EndTime,CampaignId")] InterviewInviteModel model)
    {
        if (id == null)
        {
            return NotFound();
        }

        var applicant = await _context.Applicants
            .Include(a => a.Resume)
                .ThenInclude(a => a!.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (applicant == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var meeting = new Meeting
            {
                Name = model.Label,
                Slug =  $"{FriendlyUrlHelper.GetFriendlyTitle(model.Label)}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
                Details = model.Details,
                Location = model.Location,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                EntityId = model.CampaignId,
                EntityType = EntityType.Campaign,
                CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            };
            applicant.Status = ApplicantStatus.Interview;
            _context.Update(applicant);
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();

            if (meeting != null)
            {
                var invite = new Invite
                {
                    Label = model.Label,
                    Message = model.Message,
                    Type = InviteType.Interview,
                    InviteeKey = applicant.Resume!.UserId,
                    InviteeName = applicant.Resume!.User!.FullName,
                    InviterKey = User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                    InviterName = User.FindFirst("FullName")!.Value,
                    EntityId = meeting.Id,
                    EntityType = EntityType.Meeting
                };

                _context.Add(invite);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(OpportunitiesController.Applicants), "Opportunities", new { id = applicant.OpportunityId });
        }

        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{id}/Status")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Status(long? id, [Bind("Status")] ApplicantStatusUpdateModel model)
    {
        if (id == null)
        {
            return NotFound();
        }

        var applicant = await _context.Applicants
            .Include(a => a.Opportunity)
            .Include(a => a.Resume)
                .ThenInclude(a => a!.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (applicant == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {    
           switch ((ApplicantStatus) model.Status)
            {
                case ApplicantStatus.Selected:
                    applicant.Status = (ApplicantStatus) model.Status;

                    _context.Volunteers.Add(new Volunteer
                    {
                        OpportunityId = applicant.OpportunityId,
                        UserId = applicant.Resume!.UserId
                    });
                    _context.Update(applicant);
                    _context.SaveChanges();
                    
                    break;
                case ApplicantStatus.NotSelected:
                    break;
                default:
                    break;
            }
        }

        return Redirect(HttpContext.Request.Headers.Referer!); 
    }
}