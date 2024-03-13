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
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public ProfileController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [Route("{id}")]
    public async Task<IActionResult> Basic(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

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

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var model = new UserResumeModel
        {
            Creator = await _userService.GetCreatorAsync(user.Id),
            Resume = _context.Resumes.FirstOrDefault(r => r.CreatedBy == user.Id) ?? null
        };

        return View(model);
    }

    [Route("{id}/Values")]
    public async Task<IActionResult> Values(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        ViewData["Values"] = await _context.Values.OrderBy(v => v.Name).ToListAsync();

        return View(user);
    }
}