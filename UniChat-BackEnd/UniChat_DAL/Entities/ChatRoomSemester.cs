namespace UniChat_DAL.Entities
{
    public class ChatRoomSemester
    {
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public int Semester { get; set; }
    }
}