using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Bookings.Web.Infrastructure.Helpers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class TeamsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public TeamsController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [Route("{id}/Request")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TeamRequest(long id, [Bind("Position")] TeamRequestInputModel model)
    {
        if (!_context.Teams.Any(t => t.Id == id))
        {
            return NotFound();
        }

        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

        if (team == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(model.Position) && !string.IsNullOrWhiteSpace(model.Position) && team.Positions.Contains($"{model.Position};"))
            {
                await _context.AddAsync(new TeamRequest
                {
                    Position = model.Position,
                    TeamId = team.Id,
                    CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!
                });

                await _context.SaveChangesAsync();
            }
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{id}/Add-Member")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMember(long id, [Bind("RequestId")] AddTeamMemberModel model)
    {
        if (!_context.Teams.Any(t => t.Id == id))
        {
            return NotFound();
        }

        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

        if (team == null)
        {
            return NotFound();
        }

        if (team.CreatedBy != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        if (ModelState.IsValid)
        {
            var request = _context.TeamRequests
                .Where(r => r.Id == model.RequestId && r.TeamId == team.Id)
                .FirstOrDefault();
            
            if (request != null)
            {
                await _context.AddAsync(new TeamMember
                {
                    Position = request.Position,
                    TeamId = request.TeamId,
                    MemberId = request.CreatedBy
                });
                _context.TeamRequests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("Add-Position")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPosition([Bind("Position,TeamId")] TeamPositionInputModel model)
    {
        if (ModelState.IsValid)
        {
            var team = _context.Teams
                .Where(r => r.Id == model.TeamId)
                .FirstOrDefault();
            
            if (team != null)
            {
                if(!string.IsNullOrEmpty(model.Position) || !string.IsNullOrWhiteSpace(model.Position))
                {
                    team.Positions += $"{model.Position};";
                    _context.Teams.Update(team);
                    await _context.SaveChangesAsync();
                }
            }
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}