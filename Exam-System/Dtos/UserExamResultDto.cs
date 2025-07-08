namespace Exam_System.Dtos
{
    public class UserExamResultDto
    {
    public int Id { get; set; }
        public int ExamId { get; set; }
        public ExamDto Exam { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; } 
        public int Score { get; set; }
    }
  
}
