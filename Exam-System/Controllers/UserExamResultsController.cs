using Exam_System.Database;
using Exam_System.Database.Context;
using Exam_System.Database.Models;
using Exam_System.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserExamResultsController : ControllerBase
{
    private readonly ExamSysContext _context;

    public UserExamResultsController(ExamSysContext context)
    {
        _context = context;
    }

    //=========get all

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserExamResultDto>>> GetAll()
    {
        var results = await _context.UserExamResults
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

        return Ok(results);
    }

    //========= get by id 

    [HttpGet("{id}")]
    public async Task<ActionResult<UserExamResultDto>> GetById(int id)
    {
        var result = await _context.UserExamResults
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

        if (result == null) return NotFound();

        return Ok(result);
    }

    //============create

    [HttpPost]
    public async Task<ActionResult<UserExamResultDto>> Create(UserExamResultDto dto)
    {
        var entity = new UserExamResult
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

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
    }

    //======== edit

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserExamResultDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Mismatched ID");

        var entity = await _context.UserExamResults.FindAsync(id);
        if (entity == null) return NotFound();

        entity.ExamId = dto.ExamId;
        entity.UserId = dto.UserId;
        entity.Score = dto.Score;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    //======delete ya man

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.UserExamResults.FindAsync(id);
        if (entity == null) return NotFound();

        _context.UserExamResults.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
