using Exam_System.Database.Context;
using Exam_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repositories
{
    public class GenericRepo<T>:IGenericRepo<T> where T : class
    {
       
            protected readonly ExamSysContext _context;
            protected readonly DbSet<T> _dbSet;
            public GenericRepo(ExamSysContext context)
            {
                _context = context;
                _dbSet = _context.Set<T>();
            }
           virtual public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);


            public virtual async Task Delete(int id)
            {
                 var entity = await _dbSet.FindAsync(id);
                
                _dbSet.Remove(entity);
                
            }

            virtual public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();


            virtual public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);


           virtual public void Update(T entity) => _dbSet.Update(entity);


        
    }
}
