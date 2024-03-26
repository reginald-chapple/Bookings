using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
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

    [Route("{id}")]
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _context.Posts.FirstAsync(p => p.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        var model = new PostAuthorModel
        {
            Creator = await _userService.GetCreatorAsync(post.CreatedBy),
            Post = post
        };

        return View(model);
    }
}