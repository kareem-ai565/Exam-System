using Exam_System.Database.Models;
using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IExamService
    {
        public Task<IEnumerable<Exam>> GetExamsAsync();
        public Task<ExamDto> GetExamByIdAsync(int id);
        public Task Add(Exam exam);
        
        public Task DeleteExamAsync(Exam exam);
        public Task UpdateExam(Exam exam);
    }
}
