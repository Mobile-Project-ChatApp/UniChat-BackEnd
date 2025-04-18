using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    public List<InvitationDto?> GetInvitationsByUserId(int userId)
    {
        return _invitationsRepository.GetInvitationsByUserId(userId);
    }

    public InvitationDto? GetInvitationById(int invitationId)
    {
        return _invitationsRepository.GetInvitationById(invitationId);
    }

    public InvitationDto? GetInvitationByChatRoomAndReceiver(int chatRoomId, int receiverId)
    {
        return _invitationsRepository.GetInvitationByChatRoomAndReceiver(chatRoomId, receiverId);
    }

    public bool CreateInvitation(CreateEditInvitationDto invitation)
    {
        return _invitationsRepository.CreateInvitation(invitation);
    }

    public bool DeleteInvitation(int invitationId)
    {
        return _invitationsRepository.DeleteInvitation(invitationId);
    }

    public InviteLinkDto? GetInviteLinkByCode(string inviteCode)
    {
        return _invitationsRepository.GetInviteLinkByCode(inviteCode);
    }

    public string CreateInviteLink(CreateInviteLinkDto createInviteLinkDto, int userId)
    {
        return _invitationsRepository.CreateInviteLink(createInviteLinkDto, userId);
    }






}

