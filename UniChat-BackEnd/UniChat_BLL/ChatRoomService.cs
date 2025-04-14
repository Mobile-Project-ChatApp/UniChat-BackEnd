using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL
{
    public class ChatRoomService
    {
      private readonly IChatRoomRepository _chatRoomRepository;
      private readonly UserService _userService;

      public ChatRoomService(IChatRoomRepository chatRoomRepository, UserService userService)
      {

        _chatRoomRepository = chatRoomRepository;
        _userService = userService;
      }

      public List<ChatRoomDto> GetAllChatRooms()
      {
        return _chatRoomRepository.GetAllChatRooms();
      }

      public ChatRoomDto GetChatRoomById(int id)
      {
        return _chatRoomRepository.GetChatRoomById(id);
      }

      public bool CreateChatRoom(CreateEditChatRoomDto chatRoomDto)
      {
        if (chatRoomDto == null)
        {
          throw new ArgumentNullException(nameof(chatRoomDto), "Chat room DTO cannot be null");
        }

        return _chatRoomRepository.CreateChatRoom(chatRoomDto);
      }

      public bool UpdateChatRoom(int id, CreateEditChatRoomDto chatRoomDto)
      {
        return _chatRoomRepository.UpdateChatRoom(id, chatRoomDto);
      }

      public bool DeleteChatRoom(int id)
      {
        return _chatRoomRepository.DeleteChatRoom(id);
      }

      public bool AddUserToChatRoom(int chatRoomId, int userId)
      {
        var chatRoom = _chatRoomRepository.GetChatRoomById(chatRoomId);

        if (chatRoom == null)
        {
          throw new Exception("Chat room not found");
        }

        var user = _userService.GetUserById(userId);

        if (user == null)
        {
          throw new Exception("User not found");
        }
        
        if (chatRoom.Members.Any(m => m.Id == userId))
        {
          return false;
        }
         
        if (chatRoom.ChatRoomSemesters != null && chatRoom.ChatRoomSemesters.Count > 0)
        {
          if (!chatRoom.ChatRoomSemesters.Any(cs => cs.Semester == user.Semester))
          {
            return false;
          }
        }

        if (chatRoom.ChatRoomStudies != null && chatRoom.ChatRoomStudies.Count > 0)
        {
          foreach (var study in chatRoom.ChatRoomStudies)
          {
            if (study.Study != user.Study)
            {
              return false;
            }
          }
        }

        return _chatRoomRepository.AddUserToChatRoom(chatRoomId, userId);
      }

      public bool RemoveUserFromChatRoom(int chatRoomId, int userId)
      {
        var chatRoom = _chatRoomRepository.GetChatRoomById(chatRoomId);

        if (chatRoom == null)
        {
          throw new Exception("Chat room not found");
        }

        var user = _userService.GetUserById(userId);

        if (user == null)
        {
          throw new Exception("User not found");
        }

        return _chatRoomRepository.RemoveUserFromChatRoom(chatRoomId, userId);
      }
    }
}

