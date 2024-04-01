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
public class RepliesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public RepliesController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Content,PostId,ParentId")] Reply reply)
    {
        if (ModelState.IsValid)
        {
            if (reply.ParentId <= 0)
            {
                reply.ParentId = null;
            }
            reply.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(reply);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}