namespace UniChat_DAL.Entities;

public class InviteLink
{
    public int Id { get; set; }

    public int ChatroomId { get; set; }
    public ChatRoom Chatroom { get; set; } 
    public string InviteCode { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }

    public int CreatedByUserId { get; set; }
    public UserEntity Creator { get; set; }
}
