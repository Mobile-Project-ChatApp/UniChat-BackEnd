using Microsoft.AspNetCore.Mvc;
using UniChat_BLL;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly ChatRoomService _chatRoomService;

        public ChatRoomController(ChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        } 

        [HttpGet]
        public IActionResult GetAllChatRooms()
        {
            List<ChatRoomDto> chatRooms = _chatRoomService.GetAllChatRooms();
            return Ok(chatRooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetChatRoomById(int id)
        {
            ChatRoomDto chatRoom = _chatRoomService.GetChatRoomById(id);
            if (chatRoom == null)
            {
                return NotFound();
            }
            return Ok(chatRoom);
        }
        
        [HttpPost]
        public IActionResult CreateChatRoom(CreateEditChatRoomDto chatRoomDto)
        {
            bool createdChatRoom = _chatRoomService.CreateChatRoom(chatRoomDto);
            if (!createdChatRoom)
            {
                return BadRequest();
            }

            return Ok(createdChatRoom);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateChatRoom(int id, CreateEditChatRoomDto chatRoomDto)
        {
            bool updatedChatRoom = _chatRoomService.UpdateChatRoom(id, chatRoomDto);
            if (!updatedChatRoom)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteChatRoom(int id)
        {
            bool deletedChatRoom = _chatRoomService.DeleteChatRoom(id);
            if (!deletedChatRoom)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/users/{userId}")]
        public IActionResult AddUserToChatRoom(int id, int userId)
        {
            bool addedUser = _chatRoomService.AddUserToChatRoom(id, userId);
            if (!addedUser)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        [HttpDelete("{id}/users/{userId}")]
        public IActionResult RemoveUserFromChatRoom(int id, int userId)
        {
            bool removedUser = _chatRoomService.RemoveUserFromChatRoom(id, userId);
            if (!removedUser)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}