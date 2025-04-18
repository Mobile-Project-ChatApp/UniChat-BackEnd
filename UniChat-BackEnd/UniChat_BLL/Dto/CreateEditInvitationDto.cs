namespace UniChat_BLL.Dto;

public class CreateEditInvitationDto
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public int ChatRoomId { get; set; }
}
