namespace Exam_System.Dtos
{
    public class UserExamResultDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }
}

