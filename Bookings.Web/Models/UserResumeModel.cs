using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class UserResumeModel
{
    public CreatorModel? Creator { get; set; }

    public Resume? Resume { get; set; }
}

public class UserExperienceModel
{
    public long CampaignCount { get; set; }

    public long FollowerCount { get; set; }

    public long FollowingCount { get; set; }
    
    public CreatorModel? Creator { get; set; }

    public Resume? Resume { get; set; }
}