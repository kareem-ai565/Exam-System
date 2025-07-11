namespace Exam_System.Dtos
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateOnly CreatedAt { get; set; } 
        public Guid? UserId { get; set; }
    }
}
