using Bookings.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Web.Data.EntityConfigurations
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(x => new { x.NotificationId, x.UserId });

            builder.HasOne(x => x.Notification)
                .WithMany(a => a.Users)
                .HasForeignKey(x => x.NotificationId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(a => a.Notifications)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}