using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class CampaignsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CampaignsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Problem,Goal,Beneficiaries,Importance,Solution,FundraisingGoal,CauseId")] Campaign campaign)
    {
        if (ModelState.IsValid)
        {
            campaign.Slug = $"{FriendlyUrlHelper.GetFriendlyTitle(campaign.Name)}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            campaign.ManagerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _context.AddAsync(campaign);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{slug}")]
    public async Task<IActionResult> Timeline(string slug)
    {
        if (string.IsNullOrEmpty(slug) || string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .Include(c => c.Timeline)
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
            .Include(c => c.Manager)
            .FirstOrDefaultAsync(u => u.Slug == slug );

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
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
            .Include(c => c.Milestones)
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