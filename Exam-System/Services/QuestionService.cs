using Exam_System.Database.Models;
using Exam_System.UnitOfWork;

namespace Exam_System.Services
{
    public class QuestionService
    {
        public IUnitOfWork UnitOfWork { get; }
        public QuestionService(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Question>> GetQuestionAsynx()=> await UnitOfWork.QuestionRepo.GetAllAsync();
    }
}
