using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;

namespace UniChat_DAL
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<MessageDto> GetMessagesForChatRoom(int chatRoomId)
        {
            var messages = _context.Messages
                .Where(m => m.ChatRoomId == chatRoomId)
                .Select(m => new MessageDto() {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    MessageText = m.MessageText,
                    SentAt = m.SentAt
                })
                .ToList();

            return messages;
        }

        public string SendMessage(int chatRoomId, int senderId, string messageText)
        {
            var message = new Message() {
                ChatRoomId = chatRoomId,
                SenderId = senderId,
                MessageText = messageText,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return messageText;
        }

        public bool DeleteMessage(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message == null)
            {
                return false;
            }

            _context.Messages.Remove(message);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateMessage(int messageId, string messageText)
        {
            var message = _context.Messages.Find(messageId);
            if (message == null)
            {
                return false;
            }

            message.MessageText = messageText;
            _context.SaveChanges();

            return true;
        }
    }
}
