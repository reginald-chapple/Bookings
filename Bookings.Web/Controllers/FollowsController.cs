using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Repository;
using Bookings.Web.Domain;
using Bookings.Web.Hubs;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class FollowsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationRepository _notificationRepository;

    public FollowsController(ApplicationDbContext context, INotificationRepository notificationRepository)
    {
        _context = context;
        _notificationRepository = notificationRepository;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UserId")] Relationship relationship)
    {
        if (ModelState.IsValid)
        {
            relationship.Type = RelationshipType.Follow;
            relationship.Status = RelationshipStatus.Accepted;
            relationship.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _context.Add(relationship);
            await _context.SaveChangesAsync();
            
            var notification = new Notification 
            {
                Text = $"{HttpContext.User.FindFirst("FullName")!.Value} is now following you."
            };

            _notificationRepository.Create(notification, relationship.UserId);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}