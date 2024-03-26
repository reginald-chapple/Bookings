using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class ReplyListViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public ReplyListViewComponent(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(long postId)
    {
        var replies = await _context.Replies
            .Include(r => r.Children)
            .Where(r => r.PostId == postId && r.ParentId == null)
            .ToListAsync();

        var replyList = new List<ReplyAuthorModel>();

        foreach (var item in replies)
        {
            replyList.Add(new ReplyAuthorModel
            {
                Creator = await _userService.GetCreatorAsync(item.CreatedBy),
                Reply = item
            });
        }

        return View(replyList.OrderByDescending(p => p.Reply!.Created));
    }
}