using UniChat_DAL.Entities;

public class Invitation
{
    public int Id { get; set; }

    public int SenderId { get; set; }
    public UserEntity? Sender { get; set; }

    public int ReceiverId { get; set; }
    public UserEntity? Receiver { get; set; }

    public int ChatRoomId { get; set; }
    public ChatRoom? ChatRoom { get; set; }

    public bool IsAccepted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
