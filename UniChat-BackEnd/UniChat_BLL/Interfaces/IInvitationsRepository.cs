using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IInvitationsRepository
    {
        List<InvitationDto?> GetInvitationsByUserId(int userId);
        InvitationDto? GetInvitationById(int invitationId);
        InvitationDto? GetInvitationByChatRoomAndReceiver(int chatRoomId, int receiverId);
        bool CreateInvitation(CreateEditInvitationDto invitation);
        bool DeleteInvitation(int invitationId);
        InviteLinkDto? GetInviteLinkByCode(string inviteCode);
        string CreateInviteLink(CreateInviteLinkDto inviteLink, int userId);
    }
}
