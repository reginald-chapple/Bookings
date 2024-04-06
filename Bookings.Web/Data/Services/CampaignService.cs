using System.Linq;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Data.Services;

public class CampaignService : ICampaignService
{
    private readonly ApplicationDbContext _context;

    public CampaignService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Campaign> GetCampaignsFromCauseAndSubcauses(Cause cause)
    {
        List<Campaign> campaignList = [];
        var childCampaigns = _context.Campaigns.Include(c => c.Cause).Where(c => c.Cause!.ParentId == cause.Id).ToList();
        campaignList.AddRange(cause.Campaigns);
        campaignList.AddRange(childCampaigns);
        return campaignList;
    }
}