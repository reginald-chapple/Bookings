using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Models;

public class ChatMessageResponseModel
{
    public ChatMessageResponseModel(string content, string authorName, DateTime created)
    {
        Content = content;
        AuthorName = authorName;
        Created = created.ToString("dd, MMM yyyy");
    }

    [DataType(DataType.Text)]
    public string Content { get; set; }

    public string AuthorName { get; set; }

    public string Created { get; set; }
}