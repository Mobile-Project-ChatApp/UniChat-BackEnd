namespace UniChat_DAL.Entities;

public class AnnouncementEntity
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ChatroomId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool Important { get; set; }
    public UserEntity? Sender { get; set; }
    public List<UserAnnouncementInteraction>? UserInteractions { get; set; } = new();
    public ChatRoom? Chatroom { get; set; }

}

