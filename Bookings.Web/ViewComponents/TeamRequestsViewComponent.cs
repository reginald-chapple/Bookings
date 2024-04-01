using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class TeamRequestsViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public TeamRequestsViewComponent(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(long teamId)
    {
        var requests = await _context.TeamRequests
            .Where(r => r.TeamId == teamId)
            .ToListAsync();

        var requestList = new List<TeamRequesterModel>();

        foreach (var item in requests)
        {
            requestList.Add(new TeamRequesterModel
            {
                Creator = await _userService.GetCreatorAsync(item.CreatedBy),
                Request = item
            });
        }

        return View(requestList.OrderByDescending(r => r.Request!.Created));
    }
}