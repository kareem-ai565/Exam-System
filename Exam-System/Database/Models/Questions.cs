using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_System.Database.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public List<Choise> Choises { get; set; } = new List<Choise>();
    }
}
