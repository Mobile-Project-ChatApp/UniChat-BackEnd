using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

    [HttpPost]
    [Authorize]
    public IActionResult CreateAnnouncement(CreateEditAnnouncementDto announcementDto)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int senderId))
            return BadRequest("Invalid user ID");

        announcementDto.SenderId = senderId;

        var result = _announcementService.CreateAnnouncement(announcementDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAnnouncement(int id)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int senderId))
            return BadRequest("Invalid user ID");

        AnnouncementDto? announcement = await _announcementService.GetAnnouncementById(id);
        if (announcement == null)
            return NotFound("Announcement not found.");

        if (announcement.SenderId != senderId)
            return Forbid();

        var result = _announcementService.DeleteAnnouncement(id);
        if (result)
            return Ok("Announcement deleted successfully.");
        else
            return NotFound("Announcement not found.");
    }


}
