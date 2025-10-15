using Backend_login.Data;
using Backend_login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WikiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WikiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/wiki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wiki>>> GetWikis()
        {
            return await _context.Wikis.ToListAsync();
        }

        // GET: api/wiki/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Wiki>> GetWiki(int id)
        {
            var wiki = await _context.Wikis
                //.Include(w => w.Paginas)                                  // Se quiser incluir a pesquisa de páginas relacionadas, armazenando informações da página ná wiki (Pode ser útil no Módulo 3)
                .FirstOrDefaultAsync(w => w.IdWiki == id);

            if (wiki == null)
                return NotFound();

            return wiki;
        }

        // POST: api/wiki
        [HttpPost]
        public async Task<ActionResult<Wiki>> CreateWiki(Wiki wiki)
        {
            _context.Wikis.Add(wiki);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWiki), new { id = wiki.IdWiki }, wiki);
        }

        // PUT: api/wiki/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWiki(int id, Wiki wiki)
        {
            if (id != wiki.IdWiki)
                return BadRequest();

            _context.Entry(wiki).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/wiki/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWiki(int id)
        {
            var wiki = await _context.Wikis.FindAsync(id);
            if (wiki == null)
                return NotFound();

            _context.Wikis.Remove(wiki);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
