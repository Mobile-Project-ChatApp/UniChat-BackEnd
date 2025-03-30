using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_DAL.Entities;

public class AnnouncementEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Sender { get; set; }
    public string SenderAvatar { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public bool Important { get; set; }
    public int? SenderId { get; set; }
    public UserEntity? SenderUser { get; set; }
    
    public ICollection<UserAnnouncementInteraction>? UserInteractions { get; set; }
}

public class UserAnnouncementInteraction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int AnnouncementId { get; set; }
    public AnnouncementEntity Announcement { get; set; }
    public bool IsRead { get; set; } = false;
    public bool IsSaved { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public DateTime? SavedAt { get; set; }
}