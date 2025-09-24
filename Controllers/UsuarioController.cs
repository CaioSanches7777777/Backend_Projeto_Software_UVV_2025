using Backend_login.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_login.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "User")]
        public IEnumerable<Usuario> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Usuario
            {
                Email = index.ToString(),
                Username = index.ToString()
            })
            .ToArray();
        }
    }
}
