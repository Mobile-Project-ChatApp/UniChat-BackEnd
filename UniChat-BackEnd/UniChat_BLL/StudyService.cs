using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL
{
  public class StudyService
  {
    private readonly IStudyRepository _studyRepository;

    public StudyService(IStudyRepository studyRepository)
    {
      _studyRepository = studyRepository;
    }

    public async Task<StudyDto> GetStudyByIdAsync(int studyId)
    {
      return await _studyRepository.GetStudyByIdAsync(studyId);
    }

    public async Task<IEnumerable<StudyDto>> GetAllStudiesAsync()
    {
      return await _studyRepository.GetAllStudiesAsync();
    }

    public async Task AddStudyAsync(StudyDto study)
    {
      await _studyRepository.AddStudyAsync(study);
    }

    public async Task UpdateStudyAsync(StudyDto study)
    {
      await _studyRepository.UpdateStudyAsync(study);
    }

    public async Task DeleteStudyAsync(int studyId)
    {
      await _studyRepository.DeleteStudyAsync(studyId);
    }
  }
}