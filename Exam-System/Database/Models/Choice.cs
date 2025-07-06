using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_System.Database.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string ChoiseText { get; set; } 
        public bool IsCorrect { get; set; } 

        [ForeignKey(nameof(Questions))]
        public int QuestionsId { get; set; }
        public Question Questions { get; set; } 
    }
}
