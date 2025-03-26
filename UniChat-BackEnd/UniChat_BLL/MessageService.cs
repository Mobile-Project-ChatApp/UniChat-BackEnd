using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        
        public List<MessageDto> GetMessagesForChatRoom(int chatRoomId)
        {
            return _messageRepository.GetMessagesForChatRoom(chatRoomId);
        }

        public MessageDto SendMessage(int chatRoomId, int senderId, string messageText)
        {
            MessageDto message = _messageRepository.SendMessage(chatRoomId, senderId, messageText);
            UserDto sender = _userRepository.GetUserById(senderId);
            message.Sender = sender;
            return message;
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
