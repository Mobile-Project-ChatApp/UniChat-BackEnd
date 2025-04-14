using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniChat_BLL;
using UniChat_BLL.Dto;
using UniChat_BLL.Exceptions;

namespace UniChat_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationController : Controller
    {
        private readonly InvitationService _invitationService;
        private readonly ChatRoomService _chatRoomService;
        private readonly UserService _userService;

        public InvitationController(InvitationService invitationService, ChatRoomService chatRoomService, UserService userService)
        {
            _invitationService = invitationService;
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateInvitation([FromBody] CreateEditInvitationDto invitation)
        {
            if (invitation == null || !ModelState.IsValid)
                return BadRequest("Invalid invitation data.");

            Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            invitation.SenderId = int.Parse(userIdClaim.Value);

            ChatRoomDto chatroom;
            try
            {
                chatroom = _chatRoomService.GetChatRoomById(invitation.ChatRoomId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            if (!chatroom.Members.Any(u => u.Id == invitation.SenderId))
                return BadRequest("Sender is not a member of the chat room.");

            UserDto receiver;
            try
            {
                receiver = _userService.GetUserById(invitation.ReceiverId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            if (invitation.SenderId == invitation.ReceiverId)
                return BadRequest("Sender and receiver cannot be the same.");

            if (chatroom.Members.Any(u => u.Id == invitation.ReceiverId))
                return Conflict("User is already in the chat room.");

            InvitationDto? existingInvitation = _invitationService.GetInvitationByChatRoomAndReceiver(invitation.ChatRoomId, invitation.ReceiverId);
            if (existingInvitation != null)
                return Conflict("Invitation already exists for this user in this chat room.");

            return _invitationService.CreateInvitation(invitation)
                ? Ok("Invitation created successfully.")
                : BadRequest("Failed to create invitation.");
        }
    }
}

