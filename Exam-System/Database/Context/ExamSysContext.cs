using Exam_System.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Exam_System.Database.Context
{
    public class ExamSysContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ExamSysContext(DbContextOptions<ExamSysContext> options) : base(options)
        {
           
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choises { get; set; }
        public DbSet<UserExamResult> UserExamResults { get; set; }

      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            


            base.OnModelCreating(builder);
        }

    }
}
