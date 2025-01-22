using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockMovementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovements>>> GetStockMovements()
        {
            var stockMovements = await _context.MovimientosStock.ToListAsync();
            if (!stockMovements.Any())
            {
                return NoContent();
            }
            return Ok(stockMovements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovements>> GetStockMovement(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }
            var stockMovement = await _context.MovimientosStock.FirstOrDefaultAsync(s => s.Id == id);
            if (stockMovement == null)
            {
                return NotFound($"Movimiento de stock con ID {id} no encontrado.");
            }
            return Ok(stockMovement);
        }

        [HttpPost]
        public async Task<ActionResult<StockMovements>> CreateStockMovement([FromBody] StockMovements stockMovement)
        {
            if (stockMovement == null)
            {
                return BadRequest("Movimiento de stock inválido.");
            }
            _context.MovimientosStock.Add(stockMovement);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockMovement), new { id = stockMovement.Id }, stockMovement);
        }
    }
}
