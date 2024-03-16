using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.Controllers;

public class PostsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public PostsController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [Route("Create/Commmunity-Post")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCommmunityPost([Bind("Id,Subject,Content,CommunityId")] Post post)
    {
        if (ModelState.IsValid)
        {
            post.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            _context.Add(post);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("Create/User-Post")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUserPost([Bind("Id,Subject,Content")] Post post)
    {
        if (ModelState.IsValid)
        {
            post.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            _context.Add(post);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}