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
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int requestId))
            return BadRequest("Invalid user ID");

        List<AnnouncementDto> announcements = await _announcementService.GetAllAnnouncementsByChatroom(chatroomId, requestId);
        return Ok(announcements);
    }

    [HttpGet("chatroom/{chatroomId}/important")]
    public async Task<IActionResult> GetImportantAnnouncementsByChatroom(int chatroomId)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int requestId))
            return BadRequest("Invalid user ID");

        List<AnnouncementDto> announcements = await _announcementService.GetImportantAnnouncementsByChatroomAsync(chatroomId, requestId);
        return Ok(announcements);
    }

    [HttpGet("chatroom/{chatroomId}/recent")]
    public async Task<IActionResult> GetRecentAnnouncementsByChatroom(int chatroomId)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int requestId))
            return BadRequest("Invalid user ID");

        List<AnnouncementDto> announcements = await _announcementService.GetRecentAnnouncementsByChatroomAsync(chatroomId, requestId);
        return Ok(announcements);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto announcementDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int senderId))
            return BadRequest("Invalid user ID");

        announcementDto.SenderId = senderId;

        bool result = await _announcementService.CreateAnnouncementAsync(announcementDto);
        if (result)
            return Ok("Announcement created successfully.");
        else
            return StatusCode(500, "Failed to create announcement.");
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAnnouncement(EditAnnouncementDto announcementDto, int id)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int senderId))
            return BadRequest("Invalid user ID");

        AnnouncementDto? announcement = await _announcementService.GetAnnouncementById(id, 0);
        if (announcement == null)
            return NotFound("Announcement not found.");

        if (announcement.SenderId != senderId)
            return Forbid();

        bool result = await _announcementService.UpdateAnnouncement(announcementDto, id);
        if (result)
            return Ok(announcement);
        else
            return NotFound();
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

        AnnouncementDto? announcement = await _announcementService.GetAnnouncementById(id, 0);
        if (announcement == null)
            return NotFound("Announcement not found.");

        if (announcement.SenderId != senderId)
            return Forbid();

        bool result = await _announcementService.DeleteAnnouncement(id);
        if (result)
            return Ok("Announcement deleted successfully.");
        else
            return NotFound("Announcement not found.");
    }

    [HttpPost("mark-as-read")]
    [Authorize]
    public async Task<IActionResult> MarkAnnouncementAsRead([FromBody] MarkAnnouncementAsReadDto markAnnouncementAsReadDto)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int userId))
            return BadRequest("Invalid user ID");

        markAnnouncementAsReadDto.UserId = userId;

        await _announcementService.MarkAnnouncementAsReadAsync(markAnnouncementAsReadDto.AnnouncementId, userId);

        return Ok("Announcement marked as read.");
    }


}
