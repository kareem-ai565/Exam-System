using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Repositories.Interfaces;

namespace Exam_System.Repositories
{
    public class ChoiceRepo : GenericRepo<Choice>, IChoiceRepo
    {
        public ChoiceRepo(ExamSysContext context) : base(context)
        {
        }
    }
}
