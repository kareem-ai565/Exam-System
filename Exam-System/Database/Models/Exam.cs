using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_System.Database.Models
{
    public class Exam 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<UserExamResult> UserExamResults { get; set; } = new List<UserExamResult>();
        public List<Questions> Questions { get; set; } = new List<Questions>();



    }
}
