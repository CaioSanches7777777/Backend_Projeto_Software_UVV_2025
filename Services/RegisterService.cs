using System;
using System.Threading.Tasks;
using Backend_login.Models;
using Backend_login.DTOs;

namespace Backend_login.Services
{
    public interface IRegisterService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto dto);
    }

    public class RegisterService : IRegisterService
    {
        

        public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3)
                throw new ApplicationException("O nome é obrigatório e deve ter pelo menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new ApplicationException("E-mail inválido.");

            /*if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                throw new ApplicationException("E-mail já cadastrado.");*/

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new ApplicationException("A senha deve ter pelo menos 6 caracteres.");

            var user = new Usuario
            {
                Id = Guid.NewGuid(),
                Username = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };


            return new AuthResultDto
            {
                Success = true,
                Token = "Usuário cadastrado com sucesso. Faça login para obter um token válido."
            };
        }
    }
}
