using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class ReplyAuthorModel
{
    public virtual CreatorModel? Creator { get; set; }

    public virtual Reply? Reply { get; set; }
}