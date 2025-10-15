using Backend_login.Data;
using Backend_login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_login.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaginasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaginasController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Criar nova página
        [HttpPost]
        public async Task<IActionResult> CriarPagina([FromBody] Pagina pagina)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Pages.Add(pagina);
            await _context.SaveChangesAsync();

            return Ok(pagina);
        }

        // ✅ Buscar todas as páginas
        [HttpGet]
        public async Task<IActionResult> ListarPaginas()
        {
            var paginas = await _context.Pages.ToListAsync();
            return Ok(paginas);
        }

        // ✅ Buscar página específica (com chave composta)
        [HttpGet("{idPagina:int}/{idWiki:int}")]
        public async Task<IActionResult> BuscarPagina(int idPagina, int idWiki)
        {
            var pagina = await _context.Pages
                .FirstOrDefaultAsync(p => p.IdPagina == idPagina && p.IdWiki == idWiki);

            if (pagina == null)
                return NotFound();

            return Ok(pagina);
        }

        // ✅ Atualizar página
        [HttpPut("{idPagina:int}/{idWiki:int}")]
        public async Task<IActionResult> AtualizarPagina(int idPagina, int idWiki, [FromBody] Pagina novaPagina)
        {
            var pagina = await _context.Pages
                .FirstOrDefaultAsync(p => p.IdPagina == idPagina && p.IdWiki == idWiki);

            if (pagina == null)
                return NotFound();

            pagina.Conteudo = novaPagina.Conteudo;
            pagina.NsfwFlag = novaPagina.NsfwFlag;
            pagina.IsMain = novaPagina.IsMain;

            await _context.SaveChangesAsync();
            return Ok(pagina);
        }

        // ✅ Excluir página
        [HttpDelete("{idPagina:int}/{idWiki:int}")]
        public async Task<IActionResult> ExcluirPagina(int idPagina, int idWiki)
        {
            var pagina = await _context.Pages
                .FirstOrDefaultAsync(p => p.IdPagina == idPagina && p.IdWiki == idWiki);

            if (pagina == null)
                return NotFound();

            _context.Pages.Remove(pagina);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}