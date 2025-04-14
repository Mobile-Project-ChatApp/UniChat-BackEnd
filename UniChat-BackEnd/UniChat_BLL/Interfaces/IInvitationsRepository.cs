using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IInvitationsRepository
    {
        InvitationDto GetInvitationByChatRoomAndReceiver(int chatRoomId, int receiverId);
        bool CreateInvitation(CreateEditInvitationDto invitation);
    }
}
