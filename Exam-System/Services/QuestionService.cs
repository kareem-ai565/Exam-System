using Exam_System.Database.Models;
using Exam_System.Dtos;
using Exam_System.Services.Interfaces;
using Exam_System.UnitOfWork;

namespace Exam_System.Services
{
    public class QuestionService : IQuestionService
    {
        public IUnitOfWork UnitOfWork { get; }
        public QuestionService(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }



        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync()
        {
           var Questions  = await UnitOfWork.QuestionRepo.GetAllAsync();

            var results = new List<QuestionDto>();

            foreach (var question in Questions)
            {
                results.Add(new QuestionDto()
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    ExamId = question.ExamId,
                    Choises = question.Choises.Select(c => new ChoiceDto
                    {
                        Id = c.Id,
                        ChoiseText = c.ChoiseText,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                });
            }

            return results;
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int id)
        {
            var question = await UnitOfWork.QuestionRepo.GetByIdAsync(id);
            if (question == null)
            {
                return null;
            }
            return new QuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                ExamId = question.ExamId,
                Choises = question.Choises.Select(c => new ChoiceDto
                {
                    Id = c.Id,
                    ChoiseText = c.ChoiseText,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };
        }

        public async Task<int> Add(QuestionDto questionDto)
        {
            if (questionDto.Choises.Count(c=>c.IsCorrect) != 1)
            {
                return -1;
            }
            {
                
            }
            var question = new Question
           {
               QuestionText = questionDto.QuestionText,
               ExamId = questionDto.ExamId,
               Choises = questionDto.Choises.Select(c => new Choice
               {
                   ChoiseText = c.ChoiseText,
                   IsCorrect = c.IsCorrect
               }).ToList()
           };
             await UnitOfWork.QuestionRepo.AddAsync(question);
            return await UnitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<QuestionDto>> GetQuestionsByExamIdAsync(int examId)
        {
            var Questions = await UnitOfWork.QuestionRepo.GetQuestionsByExamIdAsync(examId);

            var results = new List<QuestionDto>();

            foreach (var question in Questions)
            {
                results.Add(new QuestionDto()
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    ExamId = question.ExamId,
                    Choises = question.Choises.Select(c => new ChoiceDto
                    {
                        Id=c.Id,
                        ChoiseText = c.ChoiseText,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                });
            }

            return results;
        }
        public async Task<int> DeleteQuestionAsync(int id)
        {
            var question = await UnitOfWork.QuestionRepo.GetByIdAsync(id);

            if (question == null)
                throw new ArgumentException($"Entity with ID {id} not found");

            await UnitOfWork.QuestionRepo.Delete(id);
            return await UnitOfWork.SaveChangesAsync();
        }

        //public async Task<int> DeleteQuestionAsync(int id)
        //{
        //    await UnitOfWork.QuestionRepo.Delete(id);
        //    return await UnitOfWork.SaveChangesAsync();
        //}

        //public Task<int> UpdateQuestion(int id, QuestionDto questionDto)
        //{
        //    var question = new Question
        //    {
        //        Id = questionDto.Id,
        //        QuestionText = questionDto.QuestionText,
        //        Choises = questionDto.Choises.Select(c => new Choice
        //        {
        //            ChoiseText = c.ChoiseText,
        //            IsCorrect = c.IsCorrect
        //        }).ToList()
        //    };
        //    return UnitOfWork.SaveChangesAsync();
        //}

        public async Task<int> UpdateQuestion(int id, QuestionDto questionDto)
        {
            // Step 1: Fetch the question including its choices
            var existingQuestion = await UnitOfWork.QuestionRepo.GetByIdAsync(id);

            if (existingQuestion == null)
                return 0; // Not found

            // Step 2: Update question text
            existingQuestion.QuestionText = questionDto.QuestionText;

            // Step 3: Replace old choices
            existingQuestion.Choises.Clear();
            foreach (var choiceDto in questionDto.Choises)
            {
                existingQuestion.Choises.Add(new Choice
                {
                    ChoiseText = choiceDto.ChoiseText,
                    IsCorrect = choiceDto.IsCorrect
                });
            }

            // Step 4: Save changes
            return await UnitOfWork.SaveChangesAsync();
        }

    }
}
