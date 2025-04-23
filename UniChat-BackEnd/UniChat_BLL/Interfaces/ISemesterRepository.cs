using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface ISemesterRepository
    {
        SemesterDto GetSemesterById(int semesterId);
        List<SemesterDto> GetAllSemesters();
        bool AddSemester(CreateEditSemesterDto semester);
        bool UpdateSemester(int id, CreateEditSemesterDto semester);
        bool DeleteSemester(int semesterId);
    }
}