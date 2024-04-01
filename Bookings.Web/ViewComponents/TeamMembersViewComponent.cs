using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class TeamMembersViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public TeamMembersViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(long teamId)
    {
        var members = await _context.TeamMembers
            .Where(r => r.TeamId == teamId)
            .Include(m => m.Member)
            .ToListAsync();

        return View(members.OrderBy(r => r.Member!.FullName));
    }
}