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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _questionService.GetQuestionsAsync();
            return questions != null ? Ok(questions) : NotFound("No Questions Found");
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto question)
        {
            if (question == null)
            {
                return BadRequest("Question cannot be null");
            }
            var result = await _questionService.Add(question);
            if (result > 0)
            {
                return Created();
                //return CreatedAtAction("GetById", new { id = question.Id });

            }else if (result == -1)
                return BadRequest("Exactly one choice must be marked as correct.");
            {
                return BadRequest("Failed to add question");
            }
            

        }

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            return question != null ? Ok(question) : NotFound("No Question Found");
        }

        [HttpGet("by-exam/{id:int}")]
        public async Task<IActionResult> GetQuestionsByExamId([FromRoute] int id)
        {
            var questions = await _questionService.GetQuestionsByExamIdAsync(id);
            return questions != null ? Ok(questions) : NotFound("No Questions Found for this Exam");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody] QuestionDto question)
        {
            if (question == null)
            {
                return BadRequest("Question cannot be null");
            }
            var result = await _questionService.UpdateQuestion(question);
            return result > 0 ? Ok("Question has been updated") : BadRequest("Failed to update question");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            return result > 0 ? Ok("Question has been deleted") : BadRequest($"Failed to delete question: {result} rows affected");
        }
    }
}
