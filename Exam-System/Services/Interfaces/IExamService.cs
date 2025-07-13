using Exam_System.Database.Models;
using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IExamService
    {
        public Task<IEnumerable<ExamDto>> GetExamsAsync();
        public Task<ExamDto> GetExamByIdAsync(int id);
        public Task<int> AddAsync(AddUpdateExamDto exam);
        public Task<int> DeleteExamAsync(int exam);
        public Task<int> UpdateExam(AddUpdateExamDto updateExamDto, int id);
    }
}
