namespace UniChat_BLL.Dto;

public class CreateEditAnnouncementDto
{
    public int SenderId { get; set; }
    public int ChatroomId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool Important { get; set; }
}
