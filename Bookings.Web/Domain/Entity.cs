using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public abstract class Entity : IEntity
{
    [ScaffoldColumn(false)]
    public DateTime Created { get; set; }

    [ScaffoldColumn(false)]
    public DateTime Updated { get; set; }

    public string EntityAge()
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(Created);

        switch (timeSpan.TotalSeconds)
        {
            case var seconds when seconds <= 60:
                return $"{timeSpan.Seconds} seconds ago";
            default:
                switch (timeSpan.TotalMinutes)
                {
                    case var minutes when minutes <= 1:
                        return "about a minute ago";
                    case var minutes when minutes < 60:
                        return $"{timeSpan.Minutes} minutes ago";
                    default:
                        switch (timeSpan.TotalHours)
                        {
                            case var hours when hours <= 1:
                                return "an hour ago";
                            case var hours when hours < 24:
                                return $"{timeSpan.Hours} hours ago";
                            default:
                                switch (timeSpan.TotalDays)
                                {
                                    case var days when days <= 2:
                                        return $"yesterday";
                                    case var days when days <= 30:
                                        return $"{timeSpan.Days} days ago";
                                    case var days when days <= 60:
                                        return "a month ago";
                                    case var days when days < 365:
                                        return $"{timeSpan.Days / 30} months ago";
                                    case var days when days <= 365 * 2:
                                        return "a year ago";
                                    default:
                                        return $"{timeSpan.Days / 365} years ago";
                                }
                        }
                }
        }

    }
}

