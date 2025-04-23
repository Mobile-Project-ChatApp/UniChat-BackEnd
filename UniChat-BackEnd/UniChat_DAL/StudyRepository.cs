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

        public StudyDto GetStudyById(int studyId)
        {
            var study = _context.Studies.Find(studyId);
            if (study == null) return null;

            return new StudyDto
            {
                Id = study.Id,
                Name = study.Name,
                Description = study.Description,
            };
        }

        public List<StudyDto> GetAllStudies()
        {
            var studies = _context.Studies.ToList();
            return studies.Select(study => new StudyDto
            {
                Id = study.Id,
                Name = study.Name,
                Description = study.Description,
            }).ToList();
        }

        public bool AddStudy(CreateEditStudyDto study)
        {
            if (study == null) return false;
            
            var newStudy = new Study
            {
                Name = study.Name,
                Description = study.Description,
            };

            var existingStudy = _context.Studies.FirstOrDefault(s => s.Name == newStudy.Name);
            if (existingStudy != null)
            {
                return false;
            }

            _context.Studies.Add(newStudy);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateStudy(int id, CreateEditStudyDto study)
        {
            var existingStudy =  _context.Studies.Find(id);
            if (existingStudy == null) return false;

            existingStudy.Name = study.Name;
            existingStudy.Description = study.Description;

            _context.Studies.Update(existingStudy);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteStudy(int studyId)
        {
            var study = _context.Studies.Find(studyId);
            if (study == null)
            {
                return false;
            }

            _context.Studies.Remove(study);
            _context.SaveChanges();

            return true;
        }
    }
}