#nullable disable
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bookings.Web.Data.EntityConfigurations;
using System.Security.Claims;

namespace Bookings.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<UserValue> UserValues { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<CommunityMember> CommunityMembers { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<ActionItem> ActionItems { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<MeetingAttendee> MeetingAttendees { get; set; }        
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<TimelineEntry> TimelineEntries { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MatchProfile> MatchProfiles { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<SkinComplexion> SkinComplexions { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.ApplyConfiguration(new ChatUserConfiguration());
            builder.ApplyConfiguration(new UserNotificationConfiguration());
            builder.ApplyConfiguration(new MeetingAttendeeConfiguration());
            builder.ApplyConfiguration(new CommunityMemberConfiguration());
            builder.ApplyConfiguration(new VolunteerConfiguration());
            builder.ApplyConfiguration(new UserValueConfiguration());
        }

        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }

            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }
            }
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
