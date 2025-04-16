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

        [HttpGet]
        [Authorize]
        [Route("byUserId")]
        public IActionResult GetInvitationsByUserId()
        {
            Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            List<InvitationDto?> invitations;

            invitations = _invitationService.GetInvitationsByUserId(userId);

            if (invitations == null)
                return NotFound();

            return Ok(invitations);
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


            ChatRoomDto? chatroom = _chatRoomService.GetChatRoomById(invitation.ChatRoomId);
            if (chatroom == null)
                return NotFound("Chat room not found.");

            UserDto receiver = _userService.GetUserById(invitation.ReceiverId);
            if (receiver == null)
                return NotFound("Receiver not found.");

            if (!chatroom.Members.Any(u => u.Id == invitation.SenderId))
                return BadRequest("Sender is not a member of the chat room.");

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

        [HttpPost]
        [Authorize]
        [Route("accept/{invitationId}")]
        public IActionResult AcceptInvitation(int invitationId)
        {
            try
            {
                Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized();
                int userId = int.Parse(userIdClaim.Value);

                InvitationDto? invitation = _invitationService.GetInvitationById(invitationId);
                if (invitation == null)
                    return NotFound("Invitation not found.");

                if (invitation.ReceiverId != userId)
                    return BadRequest("You are not the intended recipient of this invitation.");

                ChatRoomDto? chatRoom = _chatRoomService.GetChatRoomById(invitation.ChatRoomId);
                if (chatRoom == null)
                    return NotFound("Chat room not found.");

                _chatRoomService.AddUserToChatRoom(chatRoom.Id, userId);

                _invitationService.DeleteInvitation(invitation.Id);

                return Ok("Invitation accepted successfully.");
            }
            catch
            (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("decline/{invitationId}")]
        public IActionResult DeclineInvitation(int invitationId)
        {
            try
            {
                Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized();
                int userId = int.Parse(userIdClaim.Value);

                InvitationDto? invitation = _invitationService.GetInvitationById(invitationId);
                if (invitation == null)
                    return NotFound("Invitation not found.");

                if (invitation.ReceiverId != userId)
                    return BadRequest("You are not the intended recipient of this invitation.");

                _invitationService.DeleteInvitation(invitation.Id);
                return Ok("Invitation declined successfully.");
            }
            catch
            (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

