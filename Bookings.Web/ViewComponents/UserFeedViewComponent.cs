using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class UserFeedViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public UserFeedViewComponent(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string userId)
    {
        var userPosts = await _context.Posts
            .Where(p => p.CreatedBy == userId)
            .OrderBy(p => p.Created)
            .ToListAsync();

        var followingIds = _context.Relationships.Where(f => f.CreatedBy == userId && f.Type == RelationshipType.Follow).Select(f => f.UserId).ToList();

        var followingPosts = _context.Posts.Where(p => followingIds.Contains(p.CreatedBy)).OrderBy(p => p.Created).ToList();

        var feedPosts = new List<PostAuthorModel>();

        foreach (var item in userPosts)
        {
            feedPosts.Add(new PostAuthorModel
            {
                Creator = await _userService.GetCreatorAsync(item.CreatedBy),
                Post = item
            });
        }

        foreach (var item in followingPosts)
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