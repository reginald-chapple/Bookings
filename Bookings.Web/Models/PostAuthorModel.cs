using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class PostAuthorModel
{
    public virtual CreatorModel? Creator { get; set; }

    public virtual Post? Post { get; set; }
}