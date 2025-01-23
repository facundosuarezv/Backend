using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using Models.Entidades.DTOs;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public UsersController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            if (!usuarios.Any())
            {
                return NoContent();
            }

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(int id)
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

        [HttpPost("Singin")]
        public async Task<ActionResult<UserDto>> Singin(SingInDto singinDto)
        {
            if (await UserExists(singinDto.Username))
            {
                return BadRequest("El nombre de usuario ya está registrado.");
            }

            var validRoles = new[] { "USER", "ADMIN" };
            if (!validRoles.Contains(singinDto.Role.ToUpper()))
            {
                return BadRequest("El rol especificado no es válido.");
            }

            using var hmac = new HMACSHA512();
            var usuario = new User
            {
                Username = singinDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(singinDto.Password)),
                PasswordSalt = hmac.Key,
                Role = singinDto.Role.ToUpper() 
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = usuario.Username,
                Token = _tokenService.CreateToken(usuario),
                Role = usuario.Role
            };
        }


        private async Task<bool> UserExists(string username)
        {
            return await _context.Usuarios.AnyAsync(u => u.Username == username.ToLower());
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Username == loginDto.Username.ToLower());
            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado");
            }
            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Password incorrecto");
            }
            return new UserDto
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(usuario),
                Role = usuario.Role
            };

        }
    }
}
