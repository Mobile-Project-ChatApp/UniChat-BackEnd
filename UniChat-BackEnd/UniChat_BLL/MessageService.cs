using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
        public List<MessageDto> GetMessagesForChatRoom(int chatRoomId)
        {
            return _messageRepository.GetMessagesForChatRoom(chatRoomId);
        }

        public bool SendMessage(int chatRoomId, int senderId, string messageText)
        {
            return _messageRepository.SendMessage(chatRoomId, senderId, messageText);
        }

        public bool DeleteMessage(int messageId)
        {
            return _messageRepository.DeleteMessage(messageId);
        }

        public bool UpdateMessage(int messageId, string messageText)
        {
            return _messageRepository.UpdateMessage(messageId, messageText);
        }
    }
}
