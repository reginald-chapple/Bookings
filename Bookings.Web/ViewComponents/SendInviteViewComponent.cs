using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SendInviteViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public SendInviteViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke(long meetingId)
    {
        PopulateInvitedUserData(meetingId);
        return View(new Invite
        {
            EntityId = meetingId
        });
    }

    private void PopulateInvitedUserData(long meetingId)
    {
        var allUsers = _context.Users.Where(u => u.Email != "sudo@local.com").ToList();
        var invites = _context.Invites.Where(i => i.EntityId == meetingId && i.EntityType == EntityType.Meeting).ToList();
        var invitedUsers = new HashSet<string>(invites.Select(t => t.InviteeKey));

        var viewModel = new List<InvitedUser>();

        foreach (var user in allUsers)
        {
            viewModel.Add(new InvitedUser
            {
                InviteeKey = user.Id,
                Name = user.FullName,
                Assigned = invitedUsers.Contains(user.Id)
            });
        }
        ViewData["InvitedUsers"] = viewModel;
    }
}
