using Bookings.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class FlashMessageViewComponent : ViewComponent
{
    private readonly FlashMessageService _flashMessageService;

    public FlashMessageViewComponent(FlashMessageService flashMessageService)
    {
        _flashMessageService = flashMessageService;
    }

    public IViewComponentResult Invoke()
    {
        var messages = _flashMessageService.GetMessages();
        return View(messages);
    }
}
