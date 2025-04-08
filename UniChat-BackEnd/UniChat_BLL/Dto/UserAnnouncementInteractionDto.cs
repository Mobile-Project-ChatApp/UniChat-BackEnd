using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_BLL.Dto
{
    public class UserAnnouncementInteractionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public int AnnouncementId { get; set; }
        public AnnouncementDto? Announcement { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsSaved { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public DateTime? SavedAt { get; set; }
    }
}

