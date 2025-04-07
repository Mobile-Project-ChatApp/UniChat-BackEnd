namespace UniChat_DAL.Entities;

public class UserChatrooms
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }
    public bool IsAdmin { get; set; } = false;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

}
