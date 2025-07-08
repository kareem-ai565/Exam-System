using Exam_System.Dtos;
using Exam_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")] // Ensures Swagger uses JSON, not text/plain
    public class UserExamResultsController : ControllerBase
    {
        private readonly IUserExamResultService _service;

        public UserExamResultsController(IUserExamResultService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserExamResultDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserExamResultDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
