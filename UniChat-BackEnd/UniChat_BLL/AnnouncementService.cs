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
    public async Task<AnnouncementDto> GetAnnouncementById(int id, int requestId)
    {
        return await _announcementRepository.GetAnnouncementById(id, requestId);
    }
    public async Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroomId, int requestId)
    {
        return await _announcementRepository.GetAllAnnouncementsByChatroom(chatroomId, requestId);
    }
    public async Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom, int requestId)
    {
        return await _announcementRepository.GetImportantAnnouncementsByChatroomAsync(chatroom, requestId);
    }
    public async Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom, int requestId)
    {
        return await _announcementRepository.GetRecentAnnouncementsByChatroomAsync(chatroom, requestId, 7);
    }

    public bool CreateAnnouncement(CreateAnnouncementDto announcementDto)
    {
        return _announcementRepository.CreateAnnouncement(announcementDto);

    }

    public bool UpdateAnnouncement(EditAnnouncementDto announcementDto, int id)
    {
        return _announcementRepository.UpdateAnnouncement(announcementDto, id);
    }

    public bool DeleteAnnouncement(int id)
    {
        return _announcementRepository.DeleteAnnouncement(id);
    }

    public async Task MarkAnnouncementAsReadAsync(int announcementId, int userId)
    {
        await _announcementRepository.MarkAnnouncementAsReadAsync(announcementId, userId);
    }
}