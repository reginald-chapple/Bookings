using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class FollowsController : Controller
{
    private readonly ApplicationDbContext _context;

    public FollowsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UserId")] Relationship relationship)
    {
        if (ModelState.IsValid)
        {
            relationship.Type = RelationshipType.Follow;
            relationship.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            _context.Add(relationship);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}