using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL;

public class InvitationService
{
    private readonly IInvitationsRepository _invitationsRepository;

    public InvitationService(IInvitationsRepository invitationsRepository)
    {
        _invitationsRepository = invitationsRepository;
    }

    public bool CreateInvitation(CreateEditInvitationDto invitation)
    {
        return _invitationsRepository.CreateInvitation(invitation);
    }
}
