namespace Bookings.Web.Models
{
    public class ChatMessageViewModel
    {
        public string AuthorId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Created { get; set; } = string.Empty;
    }
}