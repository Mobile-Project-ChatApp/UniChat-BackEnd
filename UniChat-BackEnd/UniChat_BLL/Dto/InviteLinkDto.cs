using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_BLL.Dto
{
    public class InviteLinkDto
    {
        public int Id { get; set; }

        public int ChatroomId { get; set; }
        public string InviteCode { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
