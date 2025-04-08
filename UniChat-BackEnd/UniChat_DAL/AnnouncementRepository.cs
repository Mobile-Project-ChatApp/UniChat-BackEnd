using Microsoft.EntityFrameworkCore;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using System.Linq.Expressions;

namespace UniChat_DAL
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        private static Expression<Func<AnnouncementEntity, int, AnnouncementDto>> ToDtoWithInteractions =>
        (a, requestId) => new AnnouncementDto
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
                Name = a.Chatroom.Name
            } : null,
            UserInteractions = a.UserInteractions
                .Where(ui => ui.UserId == requestId)
                .OrderByDescending(ui => ui.SavedAt ?? ui.ReadAt)
                .Select(ui => new UserAnnouncementInteractionDto
                {
                    UserId = ui.UserId,
                    AnnouncementId = ui.AnnouncementId,
                    IsRead = ui.IsRead,
                    IsSaved = ui.IsSaved,
                    ReadAt = ui.ReadAt,
                    SavedAt = ui.SavedAt
                }).ToList()
        };

        public async Task<AnnouncementDto?> GetAnnouncementById(int id, int requestId)
        {
            AnnouncementEntity? announcement = await _context.Announcements
                .Where(a => a.Id == id)
                .Include(a => a.Sender)
                .Include(a => a.Chatroom)
                .Include(a => a.UserInteractions)
                .FirstOrDefaultAsync();

            if (announcement == null) return null;

            return ToDtoWithInteractions.Compile().Invoke(announcement, requestId);
        }


        public async Task<List<AnnouncementDto>> GetAllAnnouncementsByChatroom(int chatroom, int requestId)
        {
            List<AnnouncementEntity> announcements = await _context.Announcements
                .Where(a => a.ChatroomId == chatroom)
                .OrderByDescending(a => a.DateCreated)
                .Include(a => a.Sender)
                .Include(a => a.Chatroom)
                .Include(a => a.UserInteractions)
                .ToListAsync();

            Func<AnnouncementEntity, int, AnnouncementDto> projection = ToDtoWithInteractions.Compile();

            List<AnnouncementDto> dtos = announcements
            .Select(a => projection(a, requestId))
            .ToList();

            return dtos;
        }

        public async Task<List<AnnouncementDto>> GetImportantAnnouncementsByChatroomAsync(int chatroom, int requestId)
        {
            List<AnnouncementEntity> announcements = await _context.Announcements
                .Where(a => a.ChatroomId == chatroom && a.Important)
                .OrderByDescending(a => a.DateCreated)
                .Include(a => a.Sender)
                .Include(a => a.Chatroom)
                .Include(a => a.UserInteractions)
                .ToListAsync();

            Func<AnnouncementEntity, int, AnnouncementDto> projection = ToDtoWithInteractions.Compile();

            List<AnnouncementDto> dtos = announcements
            .Select(a => projection(a, requestId))
            .ToList();

            return dtos;

        }


        public async Task<List<AnnouncementDto>> GetRecentAnnouncementsByChatroomAsync(int chatroom, int requestId, int days = 7)
        {
            DateTime cutoffDate = DateTime.UtcNow.AddDays(-days);
            List<AnnouncementEntity> announcements = await _context.Announcements
                .Where(a => a.ChatroomId == chatroom && a.DateCreated >= cutoffDate)
                .OrderByDescending(a => a.DateCreated)
                .Include(a => a.Sender)
                .Include(a => a.Chatroom)
                .Include(a => a.UserInteractions)
                .ToListAsync();

            Func<AnnouncementEntity, int, AnnouncementDto> projection = ToDtoWithInteractions.Compile();

            List<AnnouncementDto> dtos = announcements
                .Select(a => projection(a, requestId))
                .ToList();

            return dtos;
        }

        public async Task<bool> CreateAnnouncementAsync(CreateAnnouncementDto dto)
        {
            _context.Announcements.Add(new AnnouncementEntity
            {
                SenderId = dto.SenderId,
                ChatroomId = dto.ChatroomId,
                Title = dto.Title,
                Content = dto.Content,
                DateCreated = DateTime.UtcNow,
                Important = dto.Important
            });
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAnnouncement(int id)
        {
            AnnouncementEntity? announcement = _context.Announcements.Find(id);

            if (announcement == null)
            {
                return false;
            }

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAnnouncement(EditAnnouncementDto announcementDto, int id)
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
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating user", e);
            }


        }

        public async Task MarkAnnouncementAsReadAsync(int announcementId, int userId)
        {
            UserAnnouncementInteraction? interaction = await _context.UserAnnouncementInteractions
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