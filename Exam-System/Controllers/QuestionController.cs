using Exam_System.Database.Models;
using Exam_System.Dtos;
using Exam_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto question)
        {
            if (question == null)
            {
                return BadRequest("Question cannot be null");
            }
           if (await _questionService.Add(question) == 1)
            {
                return Created();
            }
            return BadRequest();
            

            

        }
    }
}
