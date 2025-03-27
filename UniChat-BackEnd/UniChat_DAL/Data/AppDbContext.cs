using Microsoft.EntityFrameworkCore;
using UniChat_DAL.Entities;

namespace UniChat_DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AnnouncementEntity> Announcements { get; set; }
        public DbSet<UserAnnouncementInteraction> UserAnnouncementInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAnnouncementInteraction>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserAnnouncementInteraction>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnnouncementInteraction>()
                .HasOne(x => x.Announcement)
                .WithMany(a => a.UserInteractions)
                .HasForeignKey(x => x.AnnouncementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnnouncementEntity>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<AnnouncementEntity>()
                .HasIndex(x => x.Important);
        }
    }
}