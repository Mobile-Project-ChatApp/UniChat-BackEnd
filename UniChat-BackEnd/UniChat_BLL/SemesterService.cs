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

    public async Task<SemesterDto> GetSemesterByIdAsync(int semesterId)
    {
      return await _semesterRepository.GetSemesterByIdAsync(semesterId);
    }

    public async Task<IEnumerable<SemesterDto>> GetAllSemestersAsync()
    {
      return await _semesterRepository.GetAllSemestersAsync();
    }

    public async Task AddSemesterAsync(SemesterDto semester)
    {
      await _semesterRepository.AddSemesterAsync(semester);
    }

    public async Task UpdateSemesterAsync(SemesterDto semester)
    {
      await _semesterRepository.UpdateSemesterAsync(semester);
    }

    public async Task DeleteSemesterAsync(int semesterId)
    {
      await _semesterRepository.DeleteSemesterAsync(semesterId);
    }
  }
}