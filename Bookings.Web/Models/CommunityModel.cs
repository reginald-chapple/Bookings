using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class CommunityDetailsModel
{
    public virtual CreatorModel? Creator { get; set; }

    public virtual Community? Community { get; set; }
}