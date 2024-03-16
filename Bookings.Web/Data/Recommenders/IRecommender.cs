namespace Bookings.Web.Data.Recommenders;

public interface IRecommender
{
    double CalculateMatchPercentage<T>(List<T> list1, List<T> list2);
}