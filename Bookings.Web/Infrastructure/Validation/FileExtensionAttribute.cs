#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Infrastructure.Validation
{
    public class PhotoExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "gif", "png" };
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                        return new ValidationResult("Allowed extensions are jpg, gif and png");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class VideoExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "mp4", "ogg", "webm" };
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                        return new ValidationResult("Allowed extensions are mp4, ogg and webm");
                }
            }

            return ValidationResult.Success;
        }
    }
}