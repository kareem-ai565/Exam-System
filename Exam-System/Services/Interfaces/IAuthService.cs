using Azure.Identity;
using Exam_System.Contracts.Results;
using Exam_System.DTO.AuthDTO;
using Exam_System.ViewModel.AuthVM;
using Microsoft.AspNetCore.Identity;

namespace Exam_System.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult>Asynclogin(loginDTO loginDTO);
        //Task<AuthResult> Asynclogout(logoutDTO logoutDTO);
        Task<AuthResult>Asyncregister(RegisterDTO registerDTO);
        Task<(bool Success, string Message)> CreateRoleAsync(string roleName);
    }
}
