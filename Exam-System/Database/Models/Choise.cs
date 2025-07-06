using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_System.Database.Models
{
    public class Choise
    {
        public int Id { get; set; }
        public string ChoiseText { get; set; } 
        public bool IsCorrect { get; set; } 

        [ForeignKey(nameof(Questions))]
        public int QuestionsId { get; set; }
        public Questions Questions { get; set; } 
    }
}
