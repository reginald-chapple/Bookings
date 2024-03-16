using Bookings.Web.Domain;

namespace Bookings.Web.Data.Recommenders;

public class UserRecommender : Recommender, IUserRecommender
{
    public List<(ApplicationUser, double)> CalculateMatches(List<ApplicationUser> users, ApplicationUser targetUser)
    {
        List<(ApplicationUser, double)> userMatches = new List<(ApplicationUser, double)>();

        foreach (var user in users)
        {
            double matchPercentage = CalculateMatchPercentage(targetUser.Values.Select(v => v.ValueId).ToList(), user.Values.Select(v => v.ValueId).ToList());
            if (matchPercentage > 0)
            {
                userMatches.Add((user, matchPercentage));
            }
        }

        // Sort the list based on match percentage in descending order
        userMatches.Sort((x, y) => y.Item2.CompareTo(x.Item2));

        return userMatches;
    }
}

