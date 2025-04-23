using Microsoft.EntityFrameworkCore;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;

namespace UniChat_DAL
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly AppDbContext _context;

        public SemesterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SemesterDto> GetSemesterByIdAsync(int semesterId)
        {
            var semester = await _context.Semesters.FindAsync(semesterId);
            if (semester == null) return null;

            return new SemesterDto
            {
                Id = semester.Id,
                Name = semester.Name,
                Description = semester.Description,
            };
        }

        public async Task<IEnumerable<SemesterDto>> GetAllSemestersAsync()
        {
            var semesters = await _context.Semesters.ToListAsync();
            return semesters.Select(semester => new SemesterDto
            {
                Id = semester.Id,
                Name = semester.Name,
                Description = semester.Description,
            }).ToList();
        }

        public async Task AddSemesterAsync(SemesterDto semester)
        {
            var newSemester = new Semester
            {
                Name = semester.Name,
                Description = semester.Description,
            };

            await _context.Semesters.AddAsync(newSemester);
            await _context.SaveChangesAsync();
            semester.Id = newSemester.Id; // Set the Id of the DTO to the newly created entity's Id
        }

        public async Task UpdateSemesterAsync(SemesterDto semester)
        {
            var existingSemester = await _context.Semesters.FindAsync(semester.Id);
            if (existingSemester != null)
            {
                existingSemester.Name = semester.Name;
                existingSemester.Description = semester.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSemesterAsync(int semesterId)
        {
            var semester = await _context.Semesters.FindAsync(semesterId);
            if (semester != null)
            {
                _context.Semesters.Remove(semester);
                await _context.SaveChangesAsync();
            }
        }
    }
}