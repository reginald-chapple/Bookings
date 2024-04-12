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
public class RelationshipsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationRepository _notificationRepository;

    public RelationshipsController(ApplicationDbContext context, INotificationRepository notificationRepository)
    {
        _context = context;
        _notificationRepository = notificationRepository;
    }

    [Route("{id}/Family")]
    public IActionResult Family(string id)
    {
        return View();
    }

    [Route("{id}/Friends")]
    public IActionResult Friends(string id)
    {
        return View();
    }

    [Route("{id}/Professional")]
    public IActionResult Professional(string id)
    {
        return View();
    }

    [Route("{id}/Following")]
    public IActionResult Following(string id)
    {
        return View();
    }

    [Route("{id}/Followers")]
    public IActionResult Followers(string id)
    {
        return View();
    }
}