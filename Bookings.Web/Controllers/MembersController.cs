using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class MembersController : Controller
{
    private readonly ApplicationDbContext _context;

    public MembersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("{id}/Campaigns")]
    public async Task<IActionResult> Campaigns(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users
            .Include(u => u.Campaigns)
                .ThenInclude(c => c.Cause)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        return View(user);
    }

    [Route("{id}/Relationships")]
    public async Task<IActionResult> Relationships(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.Include(u => u.MatchProfile).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
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

        if (id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.Include(u => u.MatchProfile).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var invites = await _context.Invites.Where(i => i.InviteeKey == user.Id).OrderBy(i => i.Created).ToListAsync();

        return View(invites);
    }

    [Route("{id}/Resume")]
    public async Task<IActionResult> Resume(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.Include(u => u.Resume).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        return View(user);
    }
}