using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;
using Backend_login.DTOs;

namespace AuthService.Services
{
    public interface ILoginService
    {
        Task<AuthResultDto> LoginAsync(LoginDto dto);
    }

    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;


        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {
            
        }
    }
}
