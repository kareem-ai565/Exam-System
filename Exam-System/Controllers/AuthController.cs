using Exam_System.Contracts.Results;
using Exam_System.DTO.AuthDTO;
using Exam_System.Services.Interfaces;
using Exam_System.ViewModel.AuthVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok(new { message = "Logged out successfully" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(new AuthResult { Successed = false, Message = "User not found." });
            }
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            return Ok(new
            {
                UserId = Guid.Parse(userId),
                Email = email,
                Roles = roles,
                Username = userName
            });




        }
    }
}
