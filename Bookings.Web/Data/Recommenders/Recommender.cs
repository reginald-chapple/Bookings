using Bookings.Web.Domain;

namespace Bookings.Web.Data.Recommenders;

public class Recommender : IRecommender
{
    public double CalculateMatchPercentage<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count == 0 || list2.Count == 0)
            return 0; // Return 0 if either list is empty

        int matchingCount = list1.Intersect(list2).Count();
        double percentage = (double)matchingCount / Math.Max(list1.Count, list2.Count) * 100;
        return percentage;
    }
}