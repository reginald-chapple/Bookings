using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Models;

public class CreatorModel
{
    public string Id { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;
}