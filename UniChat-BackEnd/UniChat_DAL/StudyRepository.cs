using Microsoft.EntityFrameworkCore;
using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;

namespace UniChat_DAL
{
    public class StudyRepository : IStudyRepository
    {
        private readonly AppDbContext _context;

        public StudyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StudyDto> GetStudyByIdAsync(int studyId)
        {
            var study = await _context.Studies.FindAsync(studyId);
            if (study == null) return null;

            return new StudyDto
            {
                Id = study.Id,
                Name = study.Name,
                Description = study.Description,
            };
        }

        public async Task<IEnumerable<StudyDto>> GetAllStudiesAsync()
        {
            var studies = await _context.Studies.ToListAsync();
            return studies.Select(study => new StudyDto
            {
                Id = study.Id,
                Name = study.Name,
                Description = study.Description,
            }).ToList();
        }

        public async Task AddStudyAsync(StudyDto study)
        {
            var newStudy = new Study
            {
                Name = study.Name,
                Description = study.Description,
            };

            await _context.Studies.AddAsync(newStudy);
            await _context.SaveChangesAsync();
            study.Id = newStudy.Id; // Set the Id of the DTO to the newly created entity's Id
        }

        public async Task UpdateStudyAsync(StudyDto study)
        {
            var existingStudy = await _context.Studies.FindAsync(study.Id);
            if (existingStudy != null)
            {
                existingStudy.Name = study.Name;
                existingStudy.Description = study.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteStudyAsync(int studyId)
        {
            var study = await _context.Studies.FindAsync(studyId);
            if (study != null)
            {
                _context.Studies.Remove(study);
                await _context.SaveChangesAsync();
            }
        }
    }
}