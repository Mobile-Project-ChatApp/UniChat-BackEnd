using Microsoft.EntityFrameworkCore;
using UniChat_DAL.Entities;

namespace UniChat_DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<UserChatroom> UserChatrooms { get; set; }
        public DbSet<AnnouncementEntity> Announcements { get; set; }
        public DbSet<UserAnnouncementInteraction> UserAnnouncementInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserChatroom>()
                .HasKey(uc => new { uc.UserId, uc.ChatRoomId });

            modelBuilder.Entity<UserChatroom>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChatrooms)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserChatroom>()
                .HasOne(uc => uc.ChatRoom)
                .WithMany(c => c.UserChatrooms)
                .HasForeignKey(uc => uc.ChatRoomId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ChatRoom)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatRoomId);
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