using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IStudyRepository
    {
        StudyDto GetStudyById(int studyId);
        List<StudyDto> GetAllStudies();
        bool AddStudy(CreateEditStudyDto study);
        bool UpdateStudy(int id, CreateEditStudyDto study);
        bool DeleteStudy(int studyId);
    }
}