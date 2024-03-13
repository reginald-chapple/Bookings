using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Infrastructure.Helpers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class CampaignsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public CampaignsController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Reason,Problem,Goal,Beneficiaries,Importance,Solution,FundraisingGoal,CauseId")] Campaign campaign)
    {
        if (ModelState.IsValid)
        {
            campaign.Slug = $"{FriendlyUrlHelper.GetFriendlyTitle(campaign.Name)}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            campaign.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _context.AddAsync(campaign);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{slug}")]
    public async Task<IActionResult> Public(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.Cause)
            .Include(c => c.Expenditures)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        var actionItems = _context.ActionItems
            .Where(a => a.CampaignId == campaign.Id && a.Type == ActionItemType.Milestone)
            .Include(a => a.Children)
            .ToList();

        var model = new CampaignDetailsModel
        {
            Creator = await _userService.GetCreatorAsync(campaign.CreatedBy),
            Campaign = campaign,
            ActionItems = actionItems
        };

        return View(model);
    }

    [Route("{slug}/Timeline")]
    public async Task<IActionResult> Timeline(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
    }

    [Route("{slug}/Details")]
    public async Task<IActionResult> Details(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.Cause)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        var model = new CampaignDetailsModel
        {
            Creator = await _userService.GetCreatorAsync(campaign.CreatedBy),
            Campaign = campaign
        };

        return View(model);
    }

    [Route("{slug}/Expenditures")]
    public async Task<IActionResult> Expenditures(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.Expenditures)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
    }

    [Route("{slug}/Donations")]
    public async Task<IActionResult> Donations(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.Donations)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
    }

    [Route("{slug}/Opportunities")]
    public async Task<IActionResult> Opportunities(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.Opportunities)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
    }

    [Route("{slug}/Milestones")]
    public async Task<IActionResult> Milestones(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.ActionItems)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
    }

    [Route("{id}/GetCampaign")]
    public async Task<IActionResult> GetCampaign(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns.Include(c => c.Cause).FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
        {
            return NotFound();
        }

        return new JsonResult(new
        {
            Id = campaign.Id,
            Slug = campaign.Slug,
            Name = campaign.Name,
            Goal = campaign.Goal,
            Cause = campaign.Cause!.Name
        });
    }
}