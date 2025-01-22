using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            if (!usuarios.Any())
            {
                return NoContent();
            }

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }

            return Ok(usuario);
        }

    }
}
