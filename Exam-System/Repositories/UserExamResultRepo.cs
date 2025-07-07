using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Repositories.Interfaces;

namespace Exam_System.Repositories
{
    public class UserExamResultRepo : GenericRepo<UserExamResult>, IUserExamResultRepo
    {
        public UserExamResultRepo(ExamSysContext context) : base(context)
        {
        }
    }
}
