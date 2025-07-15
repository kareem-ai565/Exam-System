using Exam_System.Database.Models;
using Exam_System.Dtos;
using Exam_System.Services.Interfaces;
using Exam_System.UnitOfWork;

namespace Exam_System.Services.Implementations
{
    public class UserExamResultService : IUserExamResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserExamResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<UserExamResultDto?> GetByIdAsync(int id)
        //{
        //    var entity = await _unitOfWork.Repository<UserExamResult>().GetByIdAsync(id);
        //    if (entity == null)
        //        return null;

        //    return new UserExamResultDto
        //    {
        //        Id = entity.Id,
        //        ExamId = entity.ExamId,
        //        UserName=entity.User?.UserName ?? "Unknown",
        //        UserId = entity.UserId,
        //        Score = entity.Score
        //    };
        //}

        public async Task<UserExamResultDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<UserExamResult>().GetByIdAsync(id);
            if (entity == null)
                return null;

            // ❌ We cannot use: GetByIdAsync(entity.UserId) because it's a Guid and GetByIdAsync expects int.
            // ✅ Instead: Get ALL users and find the matching one
            var allUsers = await _unitOfWork.Repository<User>().GetAllAsync();
            var user = allUsers.FirstOrDefault(u => u.Id == entity.UserId);

            return new UserExamResultDto
            {
                Id = entity.Id,
                ExamId = entity.ExamId,
                UserId = entity.UserId,
                UserName = user?.UserName ?? "Unknown", // Or .Name if you prefer
                Score = entity.Score
            };
        }



        public async Task<UserExamResultDto> CreateAsync(UserExamResultDto dto)
        {
            var entity = new UserExamResult
            {
                ExamId = dto.ExamId,
                UserId = dto.UserId,
                Score = dto.Score
            };

            await _unitOfWork.Repository<UserExamResult>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(UserExamResultDto dto)
        {
            var entity = await _unitOfWork.Repository<UserExamResult>().GetByIdAsync(dto.Id);
            if (entity == null)
                return false;

            entity.ExamId = dto.ExamId;
            entity.UserId = dto.UserId;
            entity.Score = dto.Score;

            _unitOfWork.Repository<UserExamResult>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<UserExamResult>().GetByIdAsync(id);
            if (entity == null)
                return false;

            await _unitOfWork.Repository<UserExamResult>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        //public async Task<List<UserExamResultDto>> GetAllAsync()
        //{
        //    var results = await _unitOfWork.Repository<UserExamResult>().GetAllAsync();

        //    return results.Select(r => new UserExamResultDto
        //    {
        //        Id = r.Id,
        //        ExamId = r.ExamId,
        //        UserName = r.User?.UserName ?? "Unknown",
        //        UserId = r.UserId,
        //        Score = r.Score
        //    }).ToList();
        //}
        public async Task<List<UserExamResultDto>> GetAllAsync()
        {
            var results = await _unitOfWork.Repository<UserExamResult>().GetAllAsync();

            // Get all distinct UserIds
            var userIds = results.Select(r => r.UserId).Distinct().ToList();

            // Fetch all Users
            var users = await _unitOfWork.Repository<User>().GetAllAsync();

            // Create a dictionary for fast lookup
            var userDict = users.Where(u => userIds.Contains(u.Id))
                                .ToDictionary(u => u.Id, u => u.UserName); // or u.UserName if you prefer

            // Build DTOs
            return results.Select(r => new UserExamResultDto
            {
                Id = r.Id,
                ExamId = r.ExamId,
                UserId = r.UserId,
                UserName = userDict.TryGetValue(r.UserId, out var name) ? name : "Unknown",
                Score = r.Score
            }).ToList();
        }


    }
}
