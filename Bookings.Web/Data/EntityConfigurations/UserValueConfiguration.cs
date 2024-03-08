using Bookings.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Web.Data.EntityConfigurations
{
    public class UserValueConfiguration : IEntityTypeConfiguration<UserValue>
    {
        public void Configure(EntityTypeBuilder<UserValue> builder)
        {
            builder.HasKey(x => new { x.ValueId, x.UserId });

            builder.HasOne(x => x.Value)
                .WithMany(a => a.Users)
                .HasForeignKey(x => x.ValueId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(a => a.Values)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}