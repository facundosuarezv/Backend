using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{
    public class SellsController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public SellsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSell([FromBody] Sells sell)
        {
            if (sell == null)
            {
                return BadRequest("Venta inválida.");
            }
            await _context.Ventas.AddAsync(sell);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSells), new { id = sell.Id }, sell);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sells>>> GetSells()
        {
            var sells = await _context.Ventas.ToListAsync();
            if (!sells.Any())
            {
                return NoContent();
            }
            return Ok(sells);
        }
    }
}
