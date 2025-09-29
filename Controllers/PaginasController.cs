using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_login.Models;
using Backend_login.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;                // É a única biblioteca que permite incluir "_context.Pages.Include(p => p.SubPages)"


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

        [HttpPost]
        public async Task<IActionResult> CreatePage(Pagina page)
        {
            // pega o ID do usuário logado pelo token
            var userId = int.Parse(User.Claims.First(c => c.Type == "id").Value);

            page.UserId = userId;

            _context.Pages.Add(page);
            await _context.SaveChangesAsync();

            return Ok(page);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage(int id)
        {
            var page = await _context.Pages
                .Include(p => p.SubPages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null) return NotFound();

            return Ok(page);
        }
    }
}