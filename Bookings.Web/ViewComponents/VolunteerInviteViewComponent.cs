using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class VolunteerInviteViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long applicantId)
    {
        return View(new VolunteerInviteModel
        {
            ApplicantId = applicantId
        });
    }
}