using Bookings.Web.Domain;

namespace Bookings.Web.Data.Recommenders;

public interface IUserRecommender : IRecommender
{
    List<(ApplicationUser, double)> CalculateMatches(List<ApplicationUser> users, ApplicationUser targetUser);
}