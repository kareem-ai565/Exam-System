using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Repositories.Interfaces;

namespace Exam_System.Repositories
{
    public class ExamRepo : GenericRepo<Exam>, IExamRepo
    {
        public ExamRepo(ExamSysContext context) : base(context)
        {
        }
    }
}
