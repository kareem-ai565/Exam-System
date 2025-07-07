using Exam_System.Contracts.Results;
using Exam_System.DTO.AuthDTO;
using Exam_System.Services.Interfaces;
using Exam_System.ViewModel.AuthVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResult { Successed = false, Message = "Invalid input." });
            }
            var result = await _authService.Asyncregister(registerDTO);

            if (!result.Successed)
            {
               return BadRequest(result);
            }
            return Ok(result);
    
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResult { Successed = false, Message = "Invalid input." });
            }
            var result = await _authService.Asynclogin(loginDTO);

            if (!result.Successed)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] logoutDTO logoutDTO)
        {
            var result = await _authService.Asynclogout(logoutDTO);
            return Ok(result);
        }

    }
}
