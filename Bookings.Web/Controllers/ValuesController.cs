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
public class ValuesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public ValuesController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [Route("Add")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(List<long> Values)
    {
        if(ModelState.IsValid)
        {
            if (Values != null)
            {
                foreach(var value in Values)
                {
                    if (!_context.UserValues.Where(v => v.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier) && v.ValueId == value).Any())
                    {
                        await _context.AddAsync(new UserValue
                        {
                            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                            ValueId = value
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }
            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}