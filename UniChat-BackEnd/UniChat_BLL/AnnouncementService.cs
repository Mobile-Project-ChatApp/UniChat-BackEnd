using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL;

public class AnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    public AnnouncementService(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }
    public async Task<AnnouncementDto> GetAnnouncementById(int id)
    {
        return await _announcementRepository.GetAnnouncementById(id);
    }
    public async Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroomId)
    {
        return await _announcementRepository.GetAllAnnouncementsByChatroom(chatroomId);
    }
    public async Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom)
    {
        return await _announcementRepository.GetImportantAnnouncementsByChatroomAsync(chatroom);
    }
    public async Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom)
    {
        return await _announcementRepository.GetRecentAnnouncementsByChatroomAsync(chatroom, 7);
    }

    public bool CreateAnnouncement(CreateEditAnnouncementDto announcementDto)
    {
        return _announcementRepository.CreateAnnouncement(announcementDto);

    }

    public bool DeleteAnnouncement(int id)
    {
        return _announcementRepository.DeleteAnnouncement(id);
    }
}