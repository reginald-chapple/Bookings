using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class ChatContactViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly IChatService _chatService;

    public ChatContactViewComponent(ApplicationDbContext context, IChatService chatService)
    {
        _context = context;
        _chatService = chatService;
    }

    public async Task<IViewComponentResult> InvokeAsync(Chat chat)
    {
        return View(await _chatService.GetChatModelAsync(chat, HttpContext.User));
    }
}
