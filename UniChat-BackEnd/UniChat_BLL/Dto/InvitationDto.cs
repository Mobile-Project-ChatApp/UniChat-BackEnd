using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_BLL.Dto
{
    public class InvitationDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto Sender { get; set; }
        public UserDto Receiver { get; set; }
        public ChatRoomDto ChatRoom { get; set; }
    }
}
