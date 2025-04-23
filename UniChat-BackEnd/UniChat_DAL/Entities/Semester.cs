namespace UniChat_DAL.Entities
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}