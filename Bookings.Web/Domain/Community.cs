using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class Community : AuditableEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string About { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = false;

    public ICollection<CommunityMember> Members { get; set; } = [];
    public ICollection<Topic> Topics { get; set; } = [];
}