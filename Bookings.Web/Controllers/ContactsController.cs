using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Data.Repository;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class ContactsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationRepository _notificationRepository;

    public ContactsController(ApplicationDbContext context, INotificationRepository notificationRepository)
    {
        _context = context;
        _notificationRepository = notificationRepository;
    }

    [Route("Request")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SocialRequest([Bind("Id,UserId")] Relationship relationship)
    {
        if (ModelState.IsValid)
        {
            relationship.Type = RelationshipType.Social;
            relationship.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _context.Add(relationship);
            await _context.SaveChangesAsync();
            
            var notification = new Notification 
            {
                Text = $"{HttpContext.User.FindFirst("FullName")!.Value} requested to add you as a social contact."
            };

            _notificationRepository.Create(notification, relationship.UserId);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}