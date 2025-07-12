using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IUserExamResultService
    {
        Task<UserExamResultDto?> GetByIdAsync(int id);
        Task<UserExamResultDto> CreateAsync(UserExamResultDto dto);
        Task<bool> UpdateAsync(UserExamResultDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<UserExamResultDto>> GetAllAsync();

    }
}
