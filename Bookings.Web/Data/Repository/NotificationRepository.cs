using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Bookings.Web.Domain;
using Bookings.Web.Hubs;

namespace Bookings.Web.Data.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public ApplicationDbContext _context { get; }
        private IHubContext<NotificationHub> _hubContext;

        public NotificationRepository(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public void Create(Notification notification, string receiverId)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            var userNotification = new UserNotification
            {
                UserId = receiverId,
                NotificationId = notification.Id
            };

            _context.UserNotifications.Add(userNotification);
            _context.SaveChanges();

            _hubContext.Clients.User(receiverId).SendAsync("displayNotification", notification.Text);
        }

        public List<UserNotification> GetUserNotifications(string userId)
        {
            return [.. _context.UserNotifications
                .Where(u=>u.UserId.Equals(userId) && !u.IsRead)
                .Include(n=>n.Notification)];
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotifications
                .FirstOrDefault(n=>n.UserId.Equals(userId) && n.NotificationId==notificationId);
            notification!.IsRead = true;
            _context.UserNotifications.Update(notification);
            _context.SaveChanges();
        }
    }
}