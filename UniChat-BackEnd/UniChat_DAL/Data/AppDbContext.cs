using Microsoft.EntityFrameworkCore;
using UniChat_DAL.Entities;
using dotenv.net;

namespace UniChat_DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public AppDbContext() { }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DotEnv.Load();
                var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<UserChatroom> UserChatrooms { get; set; }
        public DbSet<AnnouncementEntity> Announcements { get; set; }
        public DbSet<UserAnnouncementInteraction> UserAnnouncementInteractions { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<InviteLink> InviteLinks { get; set; }

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
                .HasKey(x => new { x.UserId, x.AnnouncementId });

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
                .HasIndex(x => x.DateCreated);

            modelBuilder.Entity<AnnouncementEntity>()
                .HasIndex(x => x.Important);

            modelBuilder.Entity<AnnouncementEntity>()
                .HasOne(x => x.Chatroom)
                .WithMany(x => x.Announcements)
                .HasForeignKey(x => x.ChatroomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Sender)
                .WithMany(u => u.SentInvitations)
                .HasForeignKey(i => i.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Receiver)
                .WithMany(u => u.ReceivedInvitations)
                .HasForeignKey(i => i.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.ChatRoom)
                .WithMany(r => r.Invitations)
                .HasForeignKey(i => i.ChatRoomId);

            modelBuilder.Entity<InviteLink>()
            .HasIndex(i => i.InviteCode)
            .IsUnique();

            modelBuilder.Entity<InviteLink>()
                .HasOne(i => i.Creator)
                .WithMany()
                .HasForeignKey(i => i.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}