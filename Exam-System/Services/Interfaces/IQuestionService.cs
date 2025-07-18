﻿using Exam_System.Database.Models;
using Exam_System.Dtos;

namespace Exam_System.Services.Interfaces
{
    public interface IQuestionService
    {
        public Task<IEnumerable<QuestionDto>> GetQuestionsAsync();
        public Task<QuestionDto> GetQuestionByIdAsync(int id);
        public Task<int> Add(QuestionDto questionDto);

        public Task<int> DeleteQuestionAsync(int questionid);
        public Task<int> UpdateQuestion(int id,QuestionDto questionDto);

        public Task<IEnumerable<QuestionDto>> GetQuestionsByExamIdAsync(int examId);
    }
}
