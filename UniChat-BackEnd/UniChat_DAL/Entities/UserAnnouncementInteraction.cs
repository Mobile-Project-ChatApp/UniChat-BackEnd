using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_DAL.Entities
{
    public class UserAnnouncementInteraction
    {
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public int AnnouncementId { get; set; }
        public AnnouncementEntity? Announcement { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsSaved { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public DateTime? SavedAt { get; set; }
    }
}
