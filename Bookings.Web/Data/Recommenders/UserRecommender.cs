using Bookings.Web.Domain;

namespace Bookings.Web.Data.Recommenders;

public static class UserRecommender
{
    public static List<(ApplicationUser, double)> CalculateMatches(List<ApplicationUser> users, ApplicationUser targetUser)
    {
        List<(ApplicationUser, double)> userMatches = new List<(ApplicationUser, double)>();

        foreach (var user in users)
        {
            double matchPercentage = CalculatePercentageMatch(targetUser.Values.Select(v => v.ValueId).ToList(), user.Values.Select(v => v.ValueId).ToList());
            userMatches.Add((user, matchPercentage));
        }

        // Sort the list based on match percentage in descending order
        userMatches.Sort((x, y) => y.Item2.CompareTo(x.Item2));

        return userMatches;
    }

    public static double CalculatePercentageMatch(List<long> interests1, List<long> interests2)
    {
        // Calculate the Jaccard similarity coefficient
        var intersection = interests1.Intersect(interests2).Count();
        var union = interests1.Union(interests2).Count();

        // Calculate percentage match
        double percentageMatch = (double)intersection / union * 100;

        return percentageMatch;
    }

    // public static void Init()
    // {
    //     // Create a list of users with their sports interests
    //     List<User> users = new List<User>
    //     {
    //         new User { Username = "User1", SportsInterests = new List<string> { "Football", "Basketball", "Tennis" } },
    //         new User { Username = "User2", SportsInterests = new List<string> { "Football", "Baseball", "Soccer" } },
    //         new User { Username = "User3", SportsInterests = new List<string> { "Basketball", "Tennis", "Golf" } },
    //         // Add more users as needed
    //     };

    //     // Specify the target user for whom you want to find recommendations
    //     User targetUser = new User { Username = "TargetUser", SportsInterests = new List<string> { "Football", "Basketball", "Tennis" } };

    //     // Calculate and display the percentage match for each user
    //     List<(User, double)> userMatches = CalculateMatches(users, targetUser);

    //     Console.WriteLine("Recommendations for " + targetUser.Username + ":");
    //     foreach (var match in userMatches)
    //     {
    //         Console.WriteLine($"{match.Item1.Username}: {match.Item2}% match");
    //     }
    // }
}

