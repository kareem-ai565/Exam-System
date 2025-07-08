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


        public async Task<int> AddAsync(AddExamDto exam)
        {
            
            var entity = new Exam()
            {
                Description = exam.Description,
                Title = exam.Title,

            };
            await _unitOfWork.ExamRepo.AddAsync(entity);
           return await   _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteExamAsync(int id)
        {
            await _unitOfWork.ExamRepo.Delete(id);
           return  await _unitOfWork.SaveChangesAsync();
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

        public async Task<int> UpdateExam(UpdateExamDto updateExamDto, int id)
        {
            var exam = await _unitOfWork.ExamRepo.GetByIdAsync(id);
            exam.Title = updateExamDto.Title;
            exam.Description = updateExamDto.Description;
           
             _unitOfWork.ExamRepo.Update(exam);
           return  await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
