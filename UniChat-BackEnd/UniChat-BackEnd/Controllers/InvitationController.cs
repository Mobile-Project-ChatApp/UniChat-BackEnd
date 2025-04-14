using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationController : Controller
    {
        private readonly InvitationService _invitationService;

        public InvitationController(InvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateInvitation([FromBody] CreateEditInvitationDto invitation)
        {
            Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            invitation.SenderId = int.Parse(userIdClaim.Value);

            if (invitation == null)
            {
                return BadRequest("Invalid invitation data.");
            }

            bool result = _invitationService.CreateInvitation(invitation);

            if (result)
            {
                return Ok("Invitation created successfully.");
            }
            else
            {
                return BadRequest("Failed to create invitation.");
            }
        }
    }
}
