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
        public ActionResult<IEnumerable<User>> GetUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();

            if (!usuarios.Any())
            {
                return NoContent();
            }

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido."); 
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado."); 
            }

            return Ok(usuario); 
        }

    }
}
