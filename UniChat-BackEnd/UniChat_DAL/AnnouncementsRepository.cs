using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;

namespace UniChat_DAL.Repositories
{
    public class AnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnnouncementEntity>> GetAllAnnouncementsAsync()
        {
            return await _context.Announcements
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<AnnouncementEntity>> GetImportantAnnouncementsAsync()
        {
            return await _context.Announcements
                .Where(a => a.Important)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<AnnouncementEntity>> GetRecentAnnouncementsAsync(int days = 7)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            return await _context.Announcements
                .Where(a => a.Date >= cutoffDate)
                .OrderByDescending(a => a.Date)
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
                interaction = new UserAnnouncementInteraction
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
                interaction = new UserAnnouncementInteraction
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

        public async Task<List<AnnouncementEntity>> GetSavedAnnouncementsForUserAsync(int userId)
        {
            return await _context.UserAnnouncementInteractions
                .Where(x => x.UserId == userId && x.IsSaved)
                .Select(x => x.Announcement)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }
    }
}