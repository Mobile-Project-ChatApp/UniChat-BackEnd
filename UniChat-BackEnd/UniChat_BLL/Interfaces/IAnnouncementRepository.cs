using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<AnnouncementDto> GetAnnouncementById(int id, int requestId);
        Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroomId, int requestId);
        Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom, int requestId);
        Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom, int requestId, int days);
        bool CreateAnnouncement(CreateAnnouncementDto announcementDto);
        bool UpdateAnnouncement(EditAnnouncementDto announcementDto, int id);
        bool DeleteAnnouncement(int id);
        Task MarkAnnouncementAsReadAsync(int announcementId, int userId);
    }
}
