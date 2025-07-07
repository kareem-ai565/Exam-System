using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Repositories.Interfaces;

namespace Exam_System.Repositories
{
    public class QuestionRepo : GenericRepo<Question>, IQuestionRepo
    {
        public QuestionRepo(ExamSysContext context) : base(context)
        {
        }

        
    }
}
