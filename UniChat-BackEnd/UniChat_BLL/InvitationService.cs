using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL;

public class InvitationService
{
    private readonly IInvitationsRepository _invitationsRepository;
    private readonly IChatRoomRepository _chatRoomRepository;

    public InvitationService(IInvitationsRepository invitationsRepository, IChatRoomRepository chatRoomRepository)
    {
        _invitationsRepository = invitationsRepository;
        _chatRoomRepository = chatRoomRepository;
    }

    public InvitationDto GetInvitationByChatRoomAndReceiver(int chatRoomId, int receiverId)
    {
        return _invitationsRepository.GetInvitationByChatRoomAndReceiver(chatRoomId, receiverId);
    }

    public bool CreateInvitation(CreateEditInvitationDto invitation)
    {
        return _invitationsRepository.CreateInvitation(invitation);
    }





}

