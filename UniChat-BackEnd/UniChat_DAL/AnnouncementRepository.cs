using Microsoft.EntityFrameworkCore;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using System.Linq.Expressions;

namespace UniChat_DAL.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<AnnouncementDto?> GetAnnouncementById(int id, int requestId)
        {
            return _context.Announcements
                .Where(a => a.Id == id)
                .Select(a => new AnnouncementDto
                {
                    Id = a.Id,
                    SenderId = a.SenderId,
                    ChatroomId = a.ChatroomId,
                    Title = a.Title,
                    Content = a.Content,
                    DateCreated = a.DateCreated,
                    Important = a.Important,
                    Sender = a.Sender != null ? new UserDto
                    {
                        Id = a.Sender.Id,
                        Username = a.Sender.Username,
                        Email = a.Sender.Email
                    } : null,
                    Chatroom = a.Chatroom != null ? new ChatRoomDto
                    {
                        Id = a.Chatroom.Id,
                        Name = a.Chatroom.Name,
                    } : null
                })
                .FirstOrDefaultAsync();
        }

        public Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroom, int requestId)
        {
            return _context.Announcements
                .OrderByDescending(a => a.DateCreated)
                .Where(a => a.ChatroomId == chatroom)
                .Select(a => new AnnouncementDto
                {
                    Id = a.Id,
                    SenderId = a.SenderId,
                    ChatroomId = a.ChatroomId,
                    Title = a.Title,
                    Content = a.Content,
                    DateCreated = a.DateCreated,
                    Important = a.Important,
                    Sender = a.Sender != null ? new UserDto
                    {
                        Id = a.Sender.Id,
                        Username = a.Sender.Username,
                        Email = a.Sender.Email
                    } : null,
                    Chatroom = a.Chatroom != null ? new ChatRoomDto
                    {
                        Id = a.Chatroom.Id,
                        Name = a.Chatroom.Name,
                    } : null,
                    UserInteractions = a.UserInteractions
                        .OrderByDescending(ui => ui.SavedAt ?? ui.ReadAt)
                        .Where(ui => ui.AnnouncementId == a.Id)
                        .Where(ui => ui.UserId == requestId)
                        .Select(ui => new UserAnnouncementInteractionDto
                        {
                            UserId = ui.UserId,
                            AnnouncementId = ui.AnnouncementId,
                            IsRead = ui.IsRead,
                            IsSaved = ui.IsSaved,
                            ReadAt = ui.ReadAt,
                            SavedAt = ui.SavedAt
                        })
                        .ToList()
                })
                .ToListAsync();
        }



        public async Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom, int requestId)
        {
            return await _context.Announcements.Where(a => a.Important)
                .Where(a => a.ChatroomId == chatroom)
                .OrderByDescending(a => a.DateCreated)
                .Select(a => new AnnouncementDto
                {
                    Id = a.Id,
                    SenderId = a.SenderId,
                    ChatroomId = a.ChatroomId,
                    Title = a.Title,
                    Content = a.Content,
                    DateCreated = a.DateCreated,
                    Important = a.Important,
                    Sender = a.Sender!= null ? new UserDto
                    {
                        Id = a.Sender.Id,
                        Username = a.Sender.Username,
                        Email = a.Sender.Email
                    } : null,
                    Chatroom = a.Chatroom != null ? new ChatRoomDto
                    {
                        Id = a.Chatroom.Id,
                        Name = a.Chatroom.Name,
                    } : null,
                    UserInteractions = a.UserInteractions
                        .OrderByDescending(ui => ui.SavedAt ?? ui.ReadAt)
                        .Where(ui => ui.AnnouncementId == a.Id)
                        .Select(ui => new UserAnnouncementInteractionDto
                        {
                            UserId = ui.UserId,
                            AnnouncementId = ui.AnnouncementId,
                            IsRead = ui.IsRead,
                            IsSaved = ui.IsSaved,
                            ReadAt = ui.ReadAt,
                            SavedAt = ui.SavedAt
                        })
                        .ToList()

                })
                .ToListAsync(); 
        }


        public async Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom, int requestId, int days = 7)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-days);
            return await _context.Announcements.Where(a => a.DateCreated >= cutoffDate)
                .Where(a => a.ChatroomId == chatroom)
                .OrderByDescending(a => a.DateCreated)
                .Select(a => new AnnouncementDto
                {
                    Id = a.Id,
                    SenderId = a.SenderId,
                    ChatroomId = a.ChatroomId,
                    Title = a.Title,
                    Content = a.Content,
                    DateCreated = a.DateCreated,
                    Important = a.Important,
                    Sender = a.Sender != null ? new UserDto
                    {
                        Id = a.Sender.Id,
                        Username = a.Sender.Username,
                        Email = a.Sender.Email
                    } : null,
                    Chatroom = a.Chatroom != null ? new ChatRoomDto
                    {
                        Id = a.Chatroom.Id,
                        Name = a.Chatroom.Name,
                    } : null,
                    UserInteractions = a.UserInteractions
                        .OrderByDescending(ui => ui.SavedAt ?? ui.ReadAt)
                        .Where(ui => ui.AnnouncementId == a.Id)
                        .Select(ui => new UserAnnouncementInteractionDto
                        {
                            UserId = ui.UserId,
                            AnnouncementId = ui.AnnouncementId,
                            IsRead = ui.IsRead,
                            IsSaved = ui.IsSaved,
                            ReadAt = ui.ReadAt,
                            SavedAt = ui.SavedAt
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public bool CreateAnnouncement(CreateAnnouncementDto announcementDto)
        {
            var announcement = new AnnouncementEntity
            {
                SenderId = announcementDto.SenderId,
                ChatroomId = announcementDto.ChatroomId,
                Title = announcementDto.Title,
                Content = announcementDto.Content,
                DateCreated = DateTime.UtcNow,
                Important = announcementDto.Important
            };
            _context.Announcements.Add(announcement);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAnnouncement(int id)
        {
            var announcement = _context.Announcements.Find(id);
            if (announcement == null)
            {
                return false;
            }
            _context.Announcements.Remove(announcement);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateAnnouncement(EditAnnouncementDto announcementDto, int id)
        {
            try
            {
                AnnouncementEntity? announcement = _context.Announcements.Find(id);
                if (announcement == null)
                {
                    throw new Exception("Announcement not found");
                }

                announcement.Title = announcementDto.Title;
                announcement.Content = announcementDto.Content;
                announcement.Important = announcementDto.Important;
                
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating user", e);
            }


        }

        public async Task MarkAnnouncementAsReadAsync(int announcementId, int userId)
        {
            var interaction = await _context.UserAnnouncementInteractions
                .FirstOrDefaultAsync(x => x.AnnouncementId == announcementId && x.UserId == userId);
            if (interaction == null)
            {
                interaction = new UserAnnouncementInteraction
                {
                    UserId = userId,
                    AnnouncementId = announcementId,
                    IsRead = true,
                    ReadAt = DateTime.UtcNow
                };
                _context.UserAnnouncementInteractions.Add(interaction);
            }
            else
            {
                interaction.IsRead = true;
                interaction.ReadAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();

        }
    }
}