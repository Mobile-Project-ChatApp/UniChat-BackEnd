using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_BLL.Dto
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ChatroomId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool Important { get; set; }
        public UserDto? Sender { get; set; }
        public ChatRoomDto? Chatroom { get; set; }
        public List<UserAnnouncementInteractionDto>? UserInteractions { get; set; }
    }
}
