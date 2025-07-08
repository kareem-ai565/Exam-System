using Exam_System.Database.Models;
using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IExamService
    {
        public Task<IEnumerable<Exam>> GetExamsAsync();
        public Task<ExamDto> GetExamByIdAsync(int id);
        public Task<int> AddAsync(AddExamDto exam);
        
        public Task<int> DeleteExamAsync(int exam);
        public Task<int> UpdateExam(UpdateExamDto updateExamDto, int id);
    }
}
