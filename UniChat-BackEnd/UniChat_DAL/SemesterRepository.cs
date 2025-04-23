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

        public SemesterDto GetSemesterById(int semesterId)
        {
            var semester = _context.Semesters.Find(semesterId);
            if (semester == null) return null;

            return new SemesterDto
            {
                Id = semester.Id,
                Name = semester.Name,
                Description = semester.Description,
            };
        }

        public List<SemesterDto> GetAllSemesters()
        {
            var semesters = _context.Semesters.ToList();
            return semesters.Select(semester => new SemesterDto
            {
                Id = semester.Id,
                Name = semester.Name,
                Description = semester.Description,
            }).ToList();
        }

        public bool AddSemester(CreateEditSemesterDto semester)
        {
            if (semester == null) return false;

            var newSemester = new Semester
            {
                Name = semester.Name,
                Description = semester.Description,
            };

            var existingSemester = _context.Semesters.FirstOrDefault(s => s.Name == newSemester.Name);
            if (existingSemester != null)
            {
                return false;
            }

            _context.Semesters.Add(newSemester);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateSemester(int id, CreateEditSemesterDto semester)
        {
            var existingSemester = _context.Semesters.Find(id);
            if (existingSemester == null)
            {
                return false;
            }

            existingSemester.Name = semester.Name;
            existingSemester.Description = semester.Description;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteSemester(int semesterId)
        {
            var semester = _context.Semesters.Find(semesterId);
            if (semester == null)
            {
                return false;
            }

            _context.Semesters.Remove(semester);
            _context.SaveChanges();

            return true;
        }
    }
}