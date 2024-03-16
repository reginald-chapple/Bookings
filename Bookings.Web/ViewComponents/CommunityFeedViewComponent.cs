using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class CommunityFeedViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public CommunityFeedViewComponent(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(long communityId)
    {
        var posts = await _context.Posts
            .Where(p => p.CommunityId == communityId)
            .ToListAsync();

        var feedPosts = new List<PostAuthorModel>();

        foreach (var item in posts)
        {
            feedPosts.Add(new PostAuthorModel
            {
                Creator = await _userService.GetCreatorAsync(item.CreatedBy),
                Post = item
            });
        }

        return View(feedPosts.OrderByDescending(p => p.Post!.Created));
    }
}