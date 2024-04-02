using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class MembersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public MembersController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [Route("{id}/Campaigns")]
    public async Task<IActionResult> Campaigns(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var campaigns = await _context.Campaigns.Where(c => c.CreatedBy == user.Id).ToListAsync();
        var model = new CreatorCampaignsModel
        {
            Creator = await _userService.GetCreatorAsync(user.Id),
            Campaigns = campaigns
        };

        return View(model);
    }

    [Route("{id}/Posts")]
    public async Task<IActionResult> Posts(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        return View(user);
    }


    [Route("{id}/Communities")]
    public async Task<IActionResult> Communities(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var community = await _context.Communities.Where(c => c.CreatedBy == user.Id).FirstOrDefaultAsync();
        var model = new CommunityDetailsModel
        {
            Creator = await _userService.GetCreatorAsync(user.Id),
            Community = community
        };

        return View(model);
    }

    [Route("{id}/Relationships")]
    public async Task<IActionResult> Relationships(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        return View(user);
    }

    [Route("{id}/Invites")]
    public async Task<IActionResult> Invites(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var invites = await _context.Invites.Where(i => i.InviteeKey == user.Id).OrderBy(i => i.Created).ToListAsync();

        return View(invites);
    }

    [Route("{id}/Teams")]
    public async Task<IActionResult> Teams(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var teams = await _context.TeamMembers
            .Include(t => t.Team)
                .ThenInclude(t => t!.Campaign)
            .Where(t => t.MemberId == user.Id && t.Position != "Manager")
            .OrderByDescending(t => t.Created)
            .ToListAsync();

        var requests = await _context.TeamRequests
            .Include(t => t.Team)
                .ThenInclude(t => t!.Campaign)
            .Where(t => t.CreatedBy == user.Id)
            .OrderByDescending(t => t.Created)
            .ToListAsync();

        var model = new MemberTeams
        {
            Teams = teams,
            Requests = requests
        };

        return View(model);
    }

    [Route("{id}/Calendar")]
    public async Task<IActionResult> Calendar(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (User == null)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var claimUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (claimUserId == null)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        if (id != claimUserId)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != claimUserId)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(user);
    }

    [Route("{id}/Experience")]
    public async Task<IActionResult> Experience(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        var model = new UserExperienceModel
        {
            CampaignCount =  _context.TeamMembers.Where(c => c.MemberId == user.Id).Select(c => c.TeamId).Count(),
            FollowerCount = _context.Follows.Where(f => f.UserId == user.Id).Select(f => f.Id).Count(),
            FollowingCount = _context.Follows.Where(f => f.CreatedBy == user.Id).Select(f => f.Id).Count(),
            Creator = await _userService.GetCreatorAsync(user.Id),
            Resume = await _context.Resumes.FirstOrDefaultAsync(r => r.CreatedBy == user.Id)
        };

        return View(model);
    }
}