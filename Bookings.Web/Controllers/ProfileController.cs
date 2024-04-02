using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Recommenders;
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
    private readonly IUserRecommender _userRecommender;

    public ProfileController(ApplicationDbContext context, IUserService userService, IUserRecommender userRecommender)
    {
        _context = context;
        _userService = userService;
        _userRecommender = userRecommender;
    }

    [Route("{id}")]
    public async Task<IActionResult> Basic(string id)
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

    [Route("{id}/Resume")]
    public async Task<IActionResult> Resume(string id)
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

        var model = new UserResumeModel
        {
            Creator = await _userService.GetCreatorAsync(user.Id),
            Resume = _context.Resumes.FirstOrDefault(r => r.CreatedBy == user.Id) ?? null
        };

        return View(model);
    }

    [Route("{id}/Recommendations")]
    public async Task<IActionResult> Recommendations(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
        }

        var users = _context.Users
            .Include(u => u.Values)
            .Where(u => u.Id != User.FindFirstValue(ClaimTypes.NameIdentifier) && u.Values.Count() > 0)
            .ToList();

        var userList = new List<ApplicationUser>();

        foreach(var item in users)
        {
            if (!_context.Relationships
                .Where(f => f.CreatedBy == user.Id && f.UserId == item.Id && f.Type == RelationshipType.Follow).Any())
            {
                userList.Add(item);
            }
        }

        List<(ApplicationUser, double)> userMatches = _userRecommender.CalculateMatches(userList, user);

        ViewData["Users"] = userMatches;
        ViewData["Values"] = await _context.Values.OrderBy(v => v.Name).ToListAsync();

        return View(user);
    }
}