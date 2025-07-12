using Exam_System.Dtos;
using Exam_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserExamResultController : ControllerBase
    {
        private readonly IUserExamResultService _service;

        public UserExamResultController(IUserExamResultService service)
        {
            _service = service;
        }

        //=============== get all
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();
            return Ok(results);
        }


        // =============== get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //=============== create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserExamResultDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        //=============== update
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserExamResultDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _service.UpdateAsync(dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // =============== delete
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
