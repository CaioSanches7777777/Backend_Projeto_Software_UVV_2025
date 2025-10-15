using System;
using System.Threading.Tasks;
using Backend_login.Data;
using Backend_login.Models;
using Backend_login.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend_login.Services
{
    public interface IRegisterService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto dto);
    }

    public class RegisterService : IRegisterService
    {
        private readonly AppDbContext _db;

        public RegisterService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
        {
            // ===== Validações básicas =====
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3)
                throw new ApplicationException("O nome é obrigatório e deve ter pelo menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new ApplicationException("E-mail inválido.");

            if (await _db.Users.AnyAsync(u => u.Endereco_Email == dto.Email))
                throw new ApplicationException("E-mail já cadastrado.");

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new ApplicationException("A senha deve ter pelo menos 6 caracteres.");

            // ===== Criação do novo usuário (senha em texto puro) =====
            var user = new Usuario
            {
                Nome = dto.Name,
                Endereco_Email = dto.Email,
                Senha = dto.Password, // sem hash
                Imagem = null // pode ser ajustado depois se tiver upload de imagem
            };

            // ===== Persistência no banco =====
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // ===== Retorno =====
            return new AuthResultDto
            {
                Success = true,
                Token = "Usuário cadastrado com sucesso. Faça login para obter um token válido."
            };
        }
    }
}
