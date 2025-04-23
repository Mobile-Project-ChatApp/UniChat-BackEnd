using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IStudyRepository
    {
        Task<StudyDto> GetStudyByIdAsync(int studyId);
        Task<IEnumerable<StudyDto>> GetAllStudiesAsync();
        Task AddStudyAsync(StudyDto study);
        Task UpdateStudyAsync(StudyDto study);
        Task DeleteStudyAsync(int studyId);
    }
}