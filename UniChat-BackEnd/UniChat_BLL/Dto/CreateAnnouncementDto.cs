namespace UniChat_BLL.Dto;

public class CreateAnnouncementDto
{
    public int SenderId { get; set; }
    public int ChatroomId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool Important { get; set; }
}
