using Exam_System.Database.Models;
using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IQuestionService
    {
        public Task<IEnumerable<QuestionDto>> GetQuestionsAsync();
        //public Task<QuestionDto> GetQuestionByIdAsync(int id);
        public Task Add(QuestionDto questionDto);

        public Task DeleteQuestionAsync(QuestionDto questionDto);
        public Task UpdateQuestion(QuestionDto questionDto);
    }
}
