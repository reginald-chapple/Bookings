using Bookings.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Web.Data.EntityConfigurations
{
    public class MeetingAttendeeConfiguration : IEntityTypeConfiguration<MeetingAttendee>
    {
        public void Configure(EntityTypeBuilder<MeetingAttendee> builder)
        {
            builder.HasKey(x => new { x.MeetingId, x.AttendeeId });

            builder.HasOne(x => x.Meeting)
                .WithMany(a => a.Attendees)
                .HasForeignKey(x => x.MeetingId)
                .IsRequired();

            builder.HasOne(x => x.Attendee)
                .WithMany(a => a.Meetings)
                .HasForeignKey(x => x.AttendeeId)
                .IsRequired();
        }
    }
}