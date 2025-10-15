using Backend_login.Data;
using Backend_login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_login.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly AppDbContext _db;

        public UsuarioController(ILogger<UsuarioController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // GET /usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _db.Users.ToListAsync();
            return Ok(usuarios);
        }

        // GET /usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _db.Users.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuario);
        }

        // POST /usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario novoUsuario)
        {
            if (string.IsNullOrWhiteSpace(novoUsuario.Nome) || string.IsNullOrWhiteSpace(novoUsuario.Endereco_Email))
                return BadRequest("Nome e e-mail são obrigatórios.");

            // **Remove o hash** – senha será salva em texto puro
            // novoUsuario.Senha = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);

            _db.Users.Add(novoUsuario);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.Id_Usuario }, novoUsuario);
        }

        // DELETE /usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _db.Users.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            _db.Users.Remove(usuario);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
