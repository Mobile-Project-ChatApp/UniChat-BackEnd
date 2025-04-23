using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;

namespace UniChat_BLL
{
  public class SemesterService
  {
    private readonly ISemesterRepository _semesterRepository;

    public SemesterService(ISemesterRepository semesterRepository)
    {
      _semesterRepository = semesterRepository;
    }

    public SemesterDto GetSemesterById(int semesterId)
    {
      return _semesterRepository.GetSemesterById(semesterId);
    }

    public List<SemesterDto> GetAllSemesters()
    {
      return _semesterRepository.GetAllSemesters();
    }

    public bool AddSemester(CreateEditSemesterDto semester)
    {
      return _semesterRepository.AddSemester(semester);
    }

    public bool UpdateSemester(int id, CreateEditSemesterDto semester)
    {
      return _semesterRepository.UpdateSemester(id, semester);
    }

    public bool DeleteSemester(int semesterId)
    {
      return _semesterRepository.DeleteSemester(semesterId);
    }
  }
}