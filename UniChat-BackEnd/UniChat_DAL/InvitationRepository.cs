using UniChat_DAL.Data;
using UniChat_DAL.Entities;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_DAL;

public class InvitationRepository : IInvitationsRepository
{
    private readonly AppDbContext _context;

    public InvitationRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool CreateInvitation(CreateEditInvitationDto invitation)
    {
        var newInvitation = new Invitation
        {
            SenderId = invitation.SenderId,
            ReceiverId = invitation.ReceiverId,
            ChatRoomId = invitation.ChatRoomId,
            CreatedAt = DateTime.UtcNow,
            IsAccepted = false
        };
        _context.Invitations.Add(newInvitation);
        _context.SaveChanges();
        return true;
    }
}
