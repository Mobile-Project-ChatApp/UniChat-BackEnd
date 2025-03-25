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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<ChatRoom>()
                .HasMany(r => r.Messages)
                .WithOne()
                .HasForeignKey(m => m.ChatRoomId);
        }
    }
}
