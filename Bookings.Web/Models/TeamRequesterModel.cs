using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class TeamRequesterModel
{
    public virtual CreatorModel? Creator { get; set; }

    public virtual TeamRequest? Request { get; set; }
}