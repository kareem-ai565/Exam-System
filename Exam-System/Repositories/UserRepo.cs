using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Repositories.Interfaces;

namespace Exam_System.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(ExamSysContext context) : base(context)
        {
        }
    }
}
