using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveReplyViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long postId)
    {
        return View(new Reply
        {
            PostId = postId
        });
    }
}