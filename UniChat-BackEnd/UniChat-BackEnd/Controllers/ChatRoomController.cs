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

        [HttpPost("{id}/semesters/{semesterId}")]
        public IActionResult AddSemesterToChatRoom(int id, int semesterId)
        {
            bool addedSemester = _chatRoomService.AddSemesterToChatRoom(id, semesterId);
            if (!addedSemester)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}/semesters/{semesterId}")]
        public IActionResult RemoveSemesterFromChatRoom(int id, int semesterId)
        {
            bool removedSemester = _chatRoomService.RemoveSemesterFromChatRoom(id, semesterId);
            if (!removedSemester)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/studies/{studyId}")]
        public IActionResult AddStudyToChatRoom(int id, int studyId)
        {
            bool addedStudy = _chatRoomService.AddStudyToChatRoom(id, studyId);
            if (!addedStudy)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}/studies/{studyId}")]
        public IActionResult RemoveStudyFromChatRoom(int id, int studyId)
        {
            bool removedStudy = _chatRoomService.RemoveStudyFromChatRoom(id, studyId);
            if (!removedStudy)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}