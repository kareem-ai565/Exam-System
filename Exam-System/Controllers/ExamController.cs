using Exam_System.Database.Models;
using Exam_System.Dtos;
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
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var exam = await _examService.GetExamByIdAsync(id);

            return exam!=null?Ok(exam):NotFound("No Exam Found");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddExamDto exam)
        {
            var result = await _examService.AddAsync(exam);

            return result==0?BadRequest($"{result} rows affected"):Created();

        }

        [HttpDelete]

        public async Task<IActionResult> delete(int id)
        {   
           var result = await _examService.DeleteExamAsync(id);
            return result == 0 ? BadRequest($"{result} rows affected") : Ok("Exam has been deleted");

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateExamDto exam,[FromQuery]int id)
        {
           var result = await _examService.UpdateExam(exam, id);

            return result == 0 ? BadRequest($"{result} rows affected") : Ok("Exam has been updated");

        }
        

    }
}
