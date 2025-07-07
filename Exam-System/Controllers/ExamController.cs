using Exam_System.Database.Models;
using Exam_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exams = await _examService.GetExamsAsync();
            
            return exams!=null|| exams.Any() ?Ok(exams):NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exam = await _examService.GetExamByIdAsync(id);

            return exam!=null?Ok(exam):NotFound("No Exam Found");
        }

    }
}
