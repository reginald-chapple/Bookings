using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class NewCampaignResponseModel
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string FlashMessage { get; set; } = string.Empty;
}