using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<AnnouncementDto> GetAnnouncementById(int id);
        Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroomId);
        Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom);
        Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom, int days);
        bool CreateAnnouncement(CreateEditAnnouncementDto announcementDto);
        bool DeleteAnnouncement(int id);
    }
}
