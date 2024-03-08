namespace Bookings.Web.Data.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<string> ImageUploadAsync(IFormFile upload, string directory)
        {
            string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, $"media/{directory}");
            string imageName = Guid.NewGuid().ToString() + "_" + upload.FileName;
            string filePath = Path.Combine(uploadsDir, imageName);
            
            FileStream fs = new FileStream(filePath, FileMode.Create);
            await upload.CopyToAsync(fs);
            fs.Close();

            return imageName;
        }

        public void RemoveOldImage(string oldImage, string directory)
        {
            string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, $"media/{directory}");
            string imagePath = Path.Combine(uploadsDir, oldImage);
            File.Delete(imagePath);
        }
    }
}