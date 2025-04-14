using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IInvitationsRepository
    {
        bool CreateInvitation(CreateEditInvitationDto invitation);
    }
}
