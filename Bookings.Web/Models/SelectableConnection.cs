namespace Bookings.Web.Models
{
    public class SelectableConnection
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string UserPhoto { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
}