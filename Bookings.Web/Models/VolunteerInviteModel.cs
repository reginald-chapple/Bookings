using System.ComponentModel.DataAnnotations;
using Bookings.Web.Domain;

namespace Bookings.Web.Models;

public class VolunteerInviteModel
{
    public string Label { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Message { get; set; } = string.Empty;

    public long ApplicantId { get; set; }
}