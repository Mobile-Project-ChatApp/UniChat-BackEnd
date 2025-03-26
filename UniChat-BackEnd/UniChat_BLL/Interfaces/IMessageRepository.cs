using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IMessageRepository
    {
        List<MessageDto> GetMessagesForChatRoom(int chatRoomId);
        string SendMessage(int chatRoomId, int senderId, string messageText);
        bool DeleteMessage(int messageId);
        bool UpdateMessage(int messageId, string messageText);
    }
}
