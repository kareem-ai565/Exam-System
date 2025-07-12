using Exam_System.Contracts.Results;
using Exam_System.Database.Models;
using Exam_System.DTO.AuthDTO;
using Exam_System.Services.Interfaces;
using Exam_System.ViewModel.AuthVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exam_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration configuration;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.configuration = configuration;
        }


        public async Task<AuthResult> Asynclogin(loginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return new AuthResult
                {
                    Successed = false,
                    Message = "Invalid Credentials"
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user , roles);
            return token;
        }

        public Task<AuthResult> Asynclogout(logoutDTO logoutDTO)
        {
            return Task.FromResult(new AuthResult
            {
                Successed = true,
                Message = "Logout Successful"
            });
        }

        public async Task<AuthResult> Asyncregister(RegisterDTO registerDTO)
        {
            var userExists = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userExists != null)
            {
                return new AuthResult
                {
                    Successed = false,
                    Message = "Email is already registered."
                };
            }
            var user = new User
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(" | ",result.Errors.Select(e=>e.Description));
                return new AuthResult
                {
                    Successed = false,
                    Message = $"Registeration Failed: {errors}"
                };
            }
            await _userManager.AddToRoleAsync(user, "Student");
            return new AuthResult
            {
                Successed = true,
                Message = "Registeration Successful"
            };

        }

        private AuthResult GenerateJwtToken(User user, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:DurationInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));

            return new AuthResult
            {
                Successed = true,
                Message = "Token generated successfully",
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
