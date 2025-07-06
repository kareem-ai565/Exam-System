using Exam_System.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Database.Context
{
    public class ExamSysContext : IdentityDbContext<User>
    {
        public ExamSysContext(DbContextOptions<ExamSysContext> options) : base(options)
        {
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Choise> Choises { get; set; }
        public DbSet<UserExamResult> UserExamResults { get; set; }

    }
}
