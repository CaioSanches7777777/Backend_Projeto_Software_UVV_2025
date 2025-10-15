using Backend_login.Data;
using Backend_login.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend_login.Services
{
    public interface ILoginService
    {
        Task<AuthResultDto> LoginAsync(LoginDto dto);
    }

    public class LoginService : ILoginService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public LoginService(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {
            // Busca o usuário pelo e-mail (Endereço_Email no banco)
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Endereco_Email == dto.Email);

            // Verifica se o usuário existe e se a senha está correta (texto puro)
            if (user == null || user.Senha != dto.Password)
                throw new ApplicationException("E-mail ou senha inválidos.");

            // Cria as claims do token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id_Usuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Endereco_Email),
                new Claim("nome", user.Nome)
            };

            // Cria a chave de assinatura com base na configuração
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token JWT
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new AuthResultDto
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
