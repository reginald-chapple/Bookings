using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class OpportunitiesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public OpportunitiesController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Opportunities
        .Include(o => o.Campaign)
            .ThenInclude(c => c!.Cause)
        .ToListAsync());
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Details,CampaignId")] Opportunity opportunity)
    {
        if (ModelState.IsValid)
        {
            opportunity.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _context.AddAsync(opportunity);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{id}")]
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var opportunity = await _context.Opportunities
            .Include(o => o.Campaign)
                .ThenInclude(c => c!.Cause)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (opportunity == null)
        {
            return NotFound();
        }

        return View(opportunity);
    }

    [Route("{id}/Info")]
    public async Task<IActionResult> Info(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var opportunity = await _context.Opportunities
            .Include(o => o.Applicants)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (opportunity == null)
        {
            return NotFound();
        }

        return View(opportunity);
    }

    [Route("{id}/Applicants")]
    public async Task<IActionResult> Applicants(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var opportunity = await _context.Opportunities
            .Include(o => o.Applicants)
                .ThenInclude(a => a.Resume)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (opportunity == null)
        {
            return NotFound();
        }

        return View(opportunity);
    }

    [Route("{id}/Apply")]
    public async Task<IActionResult> Apply(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var opportunity = await _context.Opportunities.FindAsync(id);

        if (opportunity == null)
        {
            return NotFound();
        }

        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            var resumeId = _context.Resumes
                .Where(r => r.CreatedBy == User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                .Select(r => r.Id)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(resumeId) || !string.IsNullOrWhiteSpace(resumeId))
            {
                _context.Add(new Applicant
                {
                    OpportunityId = opportunity.Id,
                    ResumeId = resumeId
                });
                await _context.SaveChangesAsync();
            }

            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        
        return RedirectToAction(nameof(AccountController.Login), "Account", new { area = "Identity" });
    }

    // [Route("{oid}/Applicant/{uid}")]
    // public async Task<IActionResult> Applicant(long? oid, string uid)
    // {
    //     if (oid == null)
    //     {
    //         return NotFound();
    //     }

    //     var opportunity = await _context.Opportunities.FirstOrDefaultAsync(o => o.Id == oid);

    //     if (opportunity == null)
    //     {
    //         return NotFound();
    //     }

    //     if (string.IsNullOrEmpty(uid) || string.IsNullOrWhiteSpace(uid))
    //     {
    //         return NotFound();
    //     }

    //     var applicant = _context.Applicants.Where(a => a.)

    //     return View(opportunity);
    // }
}