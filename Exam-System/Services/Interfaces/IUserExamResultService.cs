using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IUserExamResultService
    {
        Task<IEnumerable<UserExamResultDto>> GetAllAsync();
        Task<UserExamResultDto?> GetByIdAsync(int id);
        Task<UserExamResultDto> CreateAsync(UserExamResultDto dto);
        Task<bool> UpdateAsync(int id, UserExamResultDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
