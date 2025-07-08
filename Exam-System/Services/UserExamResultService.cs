using Exam_System.Database.Context;
using Exam_System.Dtos;
using Exam_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Services
{
    public class UserExamResultService : IUserExamResultService
    {
        private readonly ExamSysContext _context;

        public UserExamResultService(ExamSysContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserExamResultDto>> GetAllAsync()
        {
            return await _context.UserExamResults
                .Include(x => x.Exam)
                .Include(x => x.User)
                .Select(x => new UserExamResultDto
                {
                    Id = x.Id,
                    ExamId = x.ExamId,
                    Exam = new ExamDto
                    {
                        Id = x.Exam.Id,
                        Title = x.Exam.Title,
                    },
                    UserId = x.UserId,
                    UserEmail = x.User.Email,
                    Score = x.Score
                }).ToListAsync();
        }

        public async Task<UserExamResultDto?> GetByIdAsync(int id)
        {
            return await _context.UserExamResults
                .Include(x => x.Exam)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .Select(x => new UserExamResultDto
                {
                    Id = x.Id,
                    ExamId = x.ExamId,
                    Exam = new ExamDto
                    {
                        Id = x.Exam.Id,
                        Title = x.Exam.Title
                    },
                    UserId = x.UserId,
                    UserEmail = x.User.Email,
                    Score = x.Score
                }).FirstOrDefaultAsync();
        }

        public async Task<UserExamResultDto> CreateAsync(UserExamResultDto dto)
        {
            var entity = new Database.Models.UserExamResult
            {
                ExamId = dto.ExamId,
                UserId = dto.UserId,
                Score = dto.Score
            };

            _context.UserExamResults.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;

            var exam = await _context.Exams.FindAsync(dto.ExamId);
            var user = await _context.Users.FindAsync(dto.UserId);

            dto.Exam = exam != null ? new ExamDto { Id = exam.Id, Title = exam.Title } : null;
            dto.UserEmail = user?.Email;

            return dto;
        }

        public async Task<bool> UpdateAsync(int id, UserExamResultDto dto)
        {
            if (id != dto.Id)
                return false;

            var entity = await _context.UserExamResults.FindAsync(id);
            if (entity == null)
                return false;

            entity.ExamId = dto.ExamId;
            entity.UserId = dto.UserId;
            entity.Score = dto.Score;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.UserExamResults.FindAsync(id);
            if (entity == null)
                return false;

            _context.UserExamResults.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
