using Exam_System.Database.Models;
using Exam_System.Dtos;
using Exam_System.Services;
using Exam_System.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
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
            var result = await _questionService.Add(question);
           if (await _questionService.Add(question) > 0)
            {
                return Created();
            }
            return BadRequest();
            

            

        }
    }
}
