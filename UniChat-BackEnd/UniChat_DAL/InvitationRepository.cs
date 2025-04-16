using UniChat_DAL.Data;
using UniChat_DAL.Entities;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using UniChat_BLL.Exceptions;

namespace UniChat_DAL;

public class InvitationRepository : IInvitationsRepository
{
    private readonly AppDbContext _context;

    public InvitationRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<InvitationDto?> GetInvitationsByUserId(int userId)
    {
        List<InvitationDto> invitations = _context.Invitations
            .Where(i => i.ReceiverId == userId)
            .Select(i => new InvitationDto
            {
                Id = i.Id,
                SenderId = i.SenderId,
                ReceiverId = i.ReceiverId,
                ChatRoomId = i.ChatRoomId,
                CreatedAt = i.CreatedAt
            })
            .ToList();
        if (invitations == null || !invitations.Any())
            return null;
        return invitations;
    } 

    public InvitationDto GetInvitationById(int invitationId)
    {
        Invitation invitation = _context.Invitations.Find(invitationId);
        if (invitation == null)
            throw new NotFoundException("Invitation not found.");
        return new InvitationDto
        {
            Id = invitation.Id,
            SenderId = invitation.SenderId,
            ReceiverId = invitation.ReceiverId,
            ChatRoomId = invitation.ChatRoomId,
            CreatedAt = invitation.CreatedAt
        };
    }

    public InvitationDto? GetInvitationByChatRoomAndReceiver(int chatRoomId, int receiverId)
    {
        Invitation? invitation = _context.Invitations
            .FirstOrDefault(i => i.ChatRoomId == chatRoomId && i.ReceiverId == receiverId);
        if (invitation == null)
            return null;
        return new InvitationDto
        {
            Id = invitation.Id,
            SenderId = invitation.SenderId,
            ReceiverId = invitation.ReceiverId,
            ChatRoomId = invitation.ChatRoomId,
            CreatedAt = invitation.CreatedAt
        };
    }

    public bool CreateInvitation(CreateEditInvitationDto invitation)
    {
        Invitation newInvitation = new Invitation
        {
            SenderId = invitation.SenderId,
            ReceiverId = invitation.ReceiverId,
            ChatRoomId = invitation.ChatRoomId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Invitations.Add(newInvitation);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteInvitation(int invitationId)
    {
        Invitation? invitation = _context.Invitations.Find(invitationId);
        if (invitation == null)
            throw new NotFoundException("Invitation not found.");
        _context.Invitations.Remove(invitation);
        _context.SaveChanges();
        return true;
    }
}
