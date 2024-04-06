using Bookings.Web.Domain;
using Bookings.Web.Models;

namespace Bookings.Web.Data.Services;

public interface ICampaignService
{
    List<Campaign> GetCampaignsFromCauseAndSubcauses(Cause cause);
}