using Exam_System.Database.Models;

namespace Exam_System.Repositories.Interfaces
{
    public interface IQuestionRepo:IGenericRepo<Question>
    {
        public Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId);
    }
}
