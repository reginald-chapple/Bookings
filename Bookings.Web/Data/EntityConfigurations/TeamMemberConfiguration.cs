using Bookings.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Web.Data.EntityConfigurations
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(x => new { x.TeamId, x.MemberId });

            builder.HasOne(x => x.Team)
                .WithMany(a => a.Members)
                .HasForeignKey(x => x.TeamId)
                .IsRequired();

            builder.HasOne(x => x.Member)
                .WithMany(a => a.Teams)
                .HasForeignKey(x => x.MemberId)
                .IsRequired();
        }
    }
}