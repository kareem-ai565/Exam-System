using Exam_System.Repositories.Interfaces;

namespace Exam_System.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo UserRepo { get; }
        IExamRepo ExamRepo { get; }

        IQuestionRepo QuestionRepo{ get; }
        IUserExamResultRepo ExamResultRepo{ get; }
        IChoiceRepo ChoiceRepo { get; }
        IGenericRepo<T> Repository<T>() where T : class;

       
        Task<int> SaveChangesAsync();


    }
}
