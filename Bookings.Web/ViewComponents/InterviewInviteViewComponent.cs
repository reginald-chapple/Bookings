using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class InterviewInviteViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long campaignId, long applicantId)
    {
        return View(new InterviewInviteModel
        {
            CampaignId = campaignId,
            ApplicantId = applicantId
        });
    }
}