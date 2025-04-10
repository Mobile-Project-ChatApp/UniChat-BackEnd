namespace UniChat_DAL.Entities
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Message> Messages { get; set; }
        public List<UserChatroom> UserChatrooms { get; set; }
        public List<AnnouncementEntity> Announcements { get; set; }
    }
}