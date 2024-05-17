using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Recommenders;
using Bookings.Web.Data.Repository;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookings.Web.Extensions;
using Newtonsoft.Json;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class RelationshipsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRecommender _userRecommender;

    public RelationshipsController(ApplicationDbContext context, INotificationRepository notificationRepository, IUserRecommender userRecommender)
    {
        _context = context;
        _notificationRepository = notificationRepository;
        _userRecommender = userRecommender;
    }
    
    [HttpPost]
    [Route("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UserId")] Relationship relationship)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(currentUserId) || string.IsNullOrWhiteSpace(currentUserId))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        if (ModelState.IsValid)
        {
            if (!_context.Relationships.Where(r => r.CreatedBy == currentUserId && r.UserId == relationship.UserId).Any() || !_context.Relationships.Where(r => r.UserId == currentUserId && r.CreatedBy == relationship.UserId).Any())
            {
                relationship.Type = RelationshipType.Social;
                relationship.CreatedBy = currentUserId;
                _context.Add(relationship);
                var notification = new Notification 
                {
                    Text = $"{HttpContext.User.FindFirst("FullName")!.Value} would like to add you as a social contact."
                };
                _notificationRepository.Create(notification, relationship.UserId);
                await _context.SaveChangesAsync();
                return Redirect(HttpContext.Request.Headers.Referer!);
            }
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{id}")]
    public async Task<IActionResult> Index(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var users = _context.Users
            .Include(u => u.Values)
            .Where(u => u.Id != User.FindFirstValue(ClaimTypes.NameIdentifier) && u.Values.Count() > 0)
            .ToList();

        var userList = new List<ApplicationUser>();

        foreach (var item in users)
        {
            if (!_context.Relationships
                .Where(f => f.CreatedBy == user.Id && f.UserId == item.Id).Any() || !_context.Relationships
                .Where(f => f.CreatedBy == item.Id && f.UserId == user.Id).Any())
            {
                userList.Add(item);
            }
        }

        List<(ApplicationUser, double)> userMatches = _userRecommender.CalculateMatches(userList, user);

        ViewData["Users"] = userMatches;

        return View(user);
    }

    [Route("{id}/Requests")]
    public async Task<IActionResult> Requests(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(user);
    }

    [Route("{id}/Social")]
    public async Task<IActionResult> Social(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(user);
    }

    [Route("{id}/Professional")]
    public async Task<IActionResult> Professional(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(user);
    }

    [Route("{id}/Following")]
    public async Task<IActionResult> Following(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(user);
    }

    [Route("{id}/Followers")]
    public async Task<IActionResult> Followers(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        var user = await _context.Users.Include(u => u.Values).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return RedirectToAction(nameof(AccountController.AccessDenied), "Account", new { area = "Identity" });
        }

        return View(user);
    }
}