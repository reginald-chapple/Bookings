namespace Bookings.Web.Data.Services
{
    public interface IFileService
    {
        Task<string> ImageUploadAsync(IFormFile upload, string directory);
        void RemoveOldImage(string oldImage, string directory);
    }
}