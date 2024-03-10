using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class UserResumeModel
{
    public CreatorModel? Creator { get; set; }

    public Resume? Resume { get; set; }
}