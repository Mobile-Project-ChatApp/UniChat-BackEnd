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

    public  StudyDto GetStudyById(int studyId)
    {
      return _studyRepository.GetStudyById(studyId);
    }

    public  List<StudyDto> GetAllStudies()
    {
      return _studyRepository.GetAllStudies();
    }

    public bool AddStudy(CreateEditStudyDto study)
    {
      return _studyRepository.AddStudy(study);
    }

    public bool UpdateStudy(int id, CreateEditStudyDto study)
    {
      return _studyRepository.UpdateStudy(id, study);
    }

    public bool DeleteStudy(int studyId)
    {
      return _studyRepository.DeleteStudy(studyId);
    }
  }
}