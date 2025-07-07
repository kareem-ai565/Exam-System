using Microsoft.AspNetCore.Identity;

namespace Exam_System.Database.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public List<UserExamResult> UserExamResults { get; set; } = new List<UserExamResult>();

        public List<Exam> CreatedExams { get; set; } = new List<Exam>();
    }
        
}

