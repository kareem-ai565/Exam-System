using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_System.Database.Models
{
    public class UserExamResult
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int Score { get; set; }
    }
}
