using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface ISemesterRepository
    {
        Task<SemesterDto> GetSemesterByIdAsync(int semesterId);
        Task<IEnumerable<SemesterDto>> GetAllSemestersAsync();
        Task AddSemesterAsync(SemesterDto semester);
        Task UpdateSemesterAsync(SemesterDto semester);
        Task DeleteSemesterAsync(int semesterId);
    }
}