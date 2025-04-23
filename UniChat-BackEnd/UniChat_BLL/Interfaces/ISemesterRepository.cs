using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface ISemesterRepository
    {
        Task<SemesterDto> GetSemesterByIdAsync(int semesterId);
        Task<IEnumerable<SemesterDto>> GetAllSemestersAsync();
        Task AddSemesterAsync(CreateEditSemesterDto semester);
        Task UpdateSemesterAsync(CreateEditSemesterDto semester);
        Task DeleteSemesterAsync(int semesterId);
    }
}