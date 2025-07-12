using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repositories
{
    public class QuestionRepo : GenericRepo<Question>, IQuestionRepo
    {
        

        public QuestionRepo(ExamSysContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId)
        {
            return await _dbSet.Where(q => q.ExamId == examId).Include(q=>q.Choises).ToListAsync();
        }

        public override async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _dbSet.Include(q=>q.Choises).ToListAsync();
        }

        public override async Task<Question?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(q => q.Choises).FirstOrDefaultAsync(q=>q.Id==id);
        }
    }
}
