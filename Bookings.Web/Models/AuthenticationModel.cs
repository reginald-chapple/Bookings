using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Models
{
    public class AuthenticationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Password { get; set; } = string.Empty;
    }
}