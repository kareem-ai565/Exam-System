using Exam_System.Database.Models;

namespace Exam_System.Dtos
{
    public class QuestionDto
    {
        public string ExamName { get; set; }
        public string QuestionText { get; set; }

        public List<Choice> Choises { get; set; } = new List<Choice>();
        

    }
}
