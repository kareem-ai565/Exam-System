using Exam_System.Database.Models;

namespace Exam_System.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int ExamId { get; set; }

        public List<ChoiceDto> Choises { get; set; }
        

    }
}
