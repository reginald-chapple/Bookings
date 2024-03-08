using Bookings.Web.Infrastructure.Validation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookings.Web.Domain;

public class ApplicationUser : IdentityUser<string>
{
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Full Name")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
    public string FullName { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    [NotMapped]
    [PhotoExtension]
    public IFormFile? ImageUpload { get; set; }

    public virtual MatchProfile? MatchProfile { get; set; }
    public virtual Resume? Resume { get; set; }

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
    public virtual ICollection<UserNotification> Notifications { get; set; } = [];
    public virtual ICollection<UserValue> Values { get; set; } = [];
    public virtual ICollection<ChatUser> Chats { get; set; } = [];
    public virtual ICollection<Campaign> Campaigns { get; set; } = [];
    public virtual ICollection<MeetingAttendee> Meetings { get; set; } = [];
    public virtual ICollection<Volunteer> VolunteerWork { get; set; } = [];
    public virtual ICollection<CommunityMember> Communities { get; set; } = [];
}
