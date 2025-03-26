namespace UniChat_DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public UserEntity Sender { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}