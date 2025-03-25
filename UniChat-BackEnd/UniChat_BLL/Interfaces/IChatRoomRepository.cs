using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IChatRoomRepository
    {
        List<ChatRoomDto> GetAllChatRooms();
        ChatRoomDto GetChatRoomById(int id);
        bool CreateChatRoom(CreateEditChatRoomDto chatRoomDto);
        bool UpdateChatRoom(int id, CreateEditChatRoomDto chatRoomDto);
        bool DeleteChatRoom(int id);
        bool AddUserToChatRoom(int chatRoomId, int userId);
        bool RemoveUserFromChatRoom(int chatRoomId, int userId);
    }
}
