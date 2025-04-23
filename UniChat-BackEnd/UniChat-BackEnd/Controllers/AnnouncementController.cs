using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniChat_BLL;
using UniChat_BLL.Dto;
using UniChat_DAL.Entities;

namespace UniChat_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly AnnouncementService _announcementService;
    private readonly ChatRoomService _chatRoomService;
    private readonly UserService _userService;

    public AnnouncementController(AnnouncementService announcementService, ChatRoomService chatRoomService, UserService userService)
    {
        _announcementService = announcementService;
        _chatRoomService = chatRoomService;
        _userService = userService;
    }


    [HttpGet("chatroom/{chatroomId}")]
    [Authorize]
    public async Task<IActionResult> GetAllAnnouncementsByChatroom(int chatroomId)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim.Value);

        ChatRoomDto? chatroom = _chatRoomService.GetChatRoomById(chatroomId);
        if (chatroom == null)
            return NotFound("Chat room not found.");

        if (!chatroom.Members.Any(u => u.Id == userId))
            return BadRequest("You're not a member of the chat room.");

        List<AnnouncementDto> announcements = await _announcementService.GetAllAnnouncementsByChatroom(chatroomId, userId);
        return Ok(announcements);
    }

    [HttpGet("chatroom/{chatroomId}/important")]
    public async Task<IActionResult> GetImportantAnnouncementsByChatroom(int chatroomId)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim.Value);

        ChatRoomDto? chatroom = _chatRoomService.GetChatRoomById(chatroomId);
        if (chatroom == null)
            return NotFound("Chat room not found.");

        if (!chatroom.Members.Any(u => u.Id == userId))
            return BadRequest("You're not a member of the chat room.");

        List<AnnouncementDto> announcements = await _announcementService.GetImportantAnnouncementsByChatroomAsync(chatroomId, userId);
        return Ok(announcements);
    }

    [HttpGet("chatroom/{chatroomId}/recent")]
    public async Task<IActionResult> GetRecentAnnouncementsByChatroom(int chatroomId)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim.Value);

        ChatRoomDto? chatroom = _chatRoomService.GetChatRoomById(chatroomId);
        if (chatroom == null)
            return NotFound("Chat room not found.");

        if (!chatroom.Members.Any(u => u.Id == userId))
            return BadRequest("You're not a member of the chat room.");

        List<AnnouncementDto> announcements = await _announcementService.GetRecentAnnouncementsByChatroomAsync(chatroomId, userId);
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

        int userId = int.Parse(userIdClaim.Value);

        ChatRoomDto? chatroom = _chatRoomService.GetChatRoomById(announcementDto.ChatroomId);
        if (chatroom == null)
            return NotFound("Chat room not found.");

        if (!chatroom.Members.Any(u => u.Id == userId))
            return BadRequest("You're not a member of the chat room.");

        announcementDto.SenderId = userId;

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

        int userId = int.Parse(userIdClaim.Value);

        AnnouncementDto? announcement = await _announcementService.GetAnnouncementById(id, 0);
        if (announcement == null)
            return NotFound("Announcement not found.");

        if (announcement.SenderId != userId)
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

        int userId = int.Parse(userIdClaim.Value);

        // Get announcement
        AnnouncementDto? announcement = await _announcementService.GetAnnouncementById(markAnnouncementAsReadDto.AnnouncementId, userId);
        if (announcement == null)
            return NotFound("Announcement not found.");

        // Get the chatroom associated with this announcement
        ChatRoomDto? chatroom = _chatRoomService.GetChatRoomById(announcement.ChatroomId);
        if (chatroom == null)
            return NotFound("Chat room not found.");

        // Check if user is a member of the chatroom
        if (!chatroom.Members.Any(u => u.Id == userId))
            return BadRequest("You're not a member of the chat room.");

        // Check if user is not the sender
        if (announcement.SenderId == userId)
            return BadRequest("You cannot mark your own announcement as read.");

        // Proceed to mark as read
        markAnnouncementAsReadDto.UserId = userId;
        await _announcementService.MarkAnnouncementAsReadAsync(markAnnouncementAsReadDto.AnnouncementId, userId);

        return Ok("Announcement marked as read.");
    }



}
