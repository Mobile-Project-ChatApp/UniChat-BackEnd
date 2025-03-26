namespace UniChat_DAL.Entities
{
    public class UserChatroom
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
