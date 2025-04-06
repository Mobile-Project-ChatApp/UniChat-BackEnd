using Microsoft.EntityFrameworkCore;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_DAL.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroom)
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



        public async Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom)
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


        public async Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom, int days = 7)
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

        public async Task<AnnouncementEntity> GetAnnouncementByIdAsync(int id)
        {
            return await _context.Announcements
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AnnouncementEntity> CreateAnnouncementAsync(AnnouncementEntity announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<AnnouncementEntity> UpdateAnnouncementAsync(AnnouncementEntity announcement)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAnnouncementForUserAsync(int announcementId, int userId)
        {
            var interaction = await _context.UserAnnouncementInteractions
                .FirstOrDefaultAsync(x => x.AnnouncementId == announcementId && x.UserId == userId);

            if (interaction == null)
            {
                interaction = new Entities.UserAnnouncementInteraction
                {
                    UserId = userId,
                    AnnouncementId = announcementId,
                    IsSaved = true,
                    SavedAt = DateTime.Now
                };
                _context.UserAnnouncementInteractions.Add(interaction);
            }
            else
            {
                interaction.IsSaved = true;
                interaction.SavedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        public async Task MarkAnnouncementAsReadAsync(int announcementId, int userId)
        {
            var interaction = await _context.UserAnnouncementInteractions
                .FirstOrDefaultAsync(x => x.AnnouncementId == announcementId && x.UserId == userId);

            if (interaction == null)
            {
                interaction = new Entities.UserAnnouncementInteraction
                {
                    UserId = userId,
                    AnnouncementId = announcementId,
                    IsRead = true,
                    ReadAt = DateTime.Now
                };
                _context.UserAnnouncementInteractions.Add(interaction);
            }
            else
            {
                interaction.IsRead = true;
                interaction.ReadAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        //public Task<List<AnnouncementDto>> GetSavedAnnouncementsForUserAsync(int userId)
        //{
        //    return _context.UserAnnouncementInteractions.Select(x => new AnnouncementDto
        //    {
        //        Id = x.Announcement.Id,
        //        SenderId = x.Announcement.SenderId,
        //        Title = x.Announcement.Title,
        //        Content = x.Announcement.Content,
        //        DateCreated = x.Announcement.DateCreated,
        //        Important = x.Announcement.Important,
        //        SenderUser = new UserDto
        //        {
        //            Id = x.Announcement.SenderUser.Id,
        //            Username = x.Announcement.SenderUser.Username,
        //            Email = x.Announcement.SenderUser.Email
        //        },
        //        UserInteractions = x.Announcement.UserInteractions.Select(ui => new UserAnnouncementInteractionDto
        //        {
        //            UserId = ui.UserId,
        //            AnnouncementId = ui.AnnouncementId,
        //            IsRead = ui.IsRead,
        //            IsSaved = ui.IsSaved,
        //            ReadAt = ui.ReadAt,
        //            SavedAt = ui.SavedAt
        //        }).ToList()
        //        .Where(x => x.UserId == userId && x.IsSaved)
        //        .Select(x => x.Announcement)
        //        .OrderByDescending(a => a.DateCreated)
        //        .ToList();
        //    });
    }
}