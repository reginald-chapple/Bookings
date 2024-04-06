using Bookings.Web.Data;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.ViewComponents;

public class CampaignListViewComponent : ViewComponent
{
    private readonly ICampaignService _campaignService;

    public CampaignListViewComponent(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    public IViewComponentResult Invoke(Cause cause)
    {
        
        return View(_campaignService.GetCampaignsFromCauseAndSubcauses(cause).AsQueryable());
    }
}