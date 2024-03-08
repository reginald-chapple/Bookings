using Bookings.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Web.Data.EntityConfigurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.HasKey(x => new { x.OpportunityId, x.UserId });

            builder.HasOne(x => x.Opportunity)
                .WithMany(a => a.Volunteers)
                .HasForeignKey(x => x.OpportunityId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(a => a.VolunteerWork)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}