namespace UniChat_DAL.Entities
{
    public class ChatRoomStudy
    {
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public int Study { get; set; }
    }
}