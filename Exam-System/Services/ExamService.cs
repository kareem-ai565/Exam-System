using Exam_System.Database.Models;
using Exam_System.Dtos;
using Exam_System.Services.Interfaces;
using Exam_System.UnitOfWork;

namespace Exam_System.Services
{
    public class ExamService :IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Exam exam)
        {
            await _unitOfWork.ExamRepo.AddAsync(exam);
          await   _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(Exam exam)
        {
           _unitOfWork.ExamRepo.Delete(exam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ExamDto> GetExamByIdAsync(int id)
        {
            var exam =  await _unitOfWork.ExamRepo.GetByIdAsync(id);

            return new ExamDto()
            {
                Id = exam.Id,
                Title = exam.Title,
                Description = exam.Description,
                CreatedAt=exam.CreatedAt
            };
        }

        

        public async Task<IEnumerable<Exam>> GetExamsAsync()=>await _unitOfWork.ExamRepo.GetAllAsync();

        public async Task UpdateExam(Exam exam)
        {
             _unitOfWork.ExamRepo.Update(exam);
            await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
