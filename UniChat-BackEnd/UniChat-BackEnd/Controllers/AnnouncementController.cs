using Microsoft.AspNetCore.Mvc;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly AnnouncementService _announcementService;

    public AnnouncementController(AnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpGet("chatroom/{chatroomId}")]
    public async Task<IActionResult> GetAllAnnouncementsByChatroom(int chatroomId)
    {
        var announcements = await _announcementService.GetAllAnnouncementsByChatroom(chatroomId);
        return Ok(announcements);
    }

    [HttpGet("chatroom/{chatroomId}/important")]
    public async Task<IActionResult> GetImportantAnnouncementsByChatroom(int chatroomId)
    {
        var announcements = await _announcementService.GetImportantAnnouncementsByChatroomAsync(chatroomId);
        return Ok(announcements);
    }

    [HttpGet("chatroom/{chatroomId}/recent")]
    public async Task<IActionResult> GetRecentAnnouncementsByChatroom(int chatroomId)
    {
        var announcements = await _announcementService.GetRecentAnnouncementsByChatroomAsync(chatroomId);
        return Ok(announcements);
    }
}
