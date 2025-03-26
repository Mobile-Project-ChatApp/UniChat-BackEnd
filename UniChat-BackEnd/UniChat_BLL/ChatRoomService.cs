using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL
{
    public class ChatRoomService
    {
      private readonly IChatRoomRepository _chatRoomRepository;

      public ChatRoomService(IChatRoomRepository chatRoomRepository)
      {
        _chatRoomRepository = chatRoomRepository;
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
        return _chatRoomRepository.AddUserToChatRoom(chatRoomId, userId);
      }

      public bool RemoveUserFromChatRoom(int chatRoomId, int userId)
      {
        return _chatRoomRepository.RemoveUserFromChatRoom(chatRoomId, userId);
      }
    }
}

