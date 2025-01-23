using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var products = await _context.Productos.ToListAsync();
            if (!products.Any())
            {
                return NoContent();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }
            var product = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Products>> CreateProduct([FromBody] Products product)
        {
            if (product == null)
            {
                return BadRequest("Producto inválido.");
            }
            _context.Productos.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Products>> UpdateProduct(int id, [FromBody] Products product)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }
            if (product == null)
            {
                return BadRequest("Producto inválido.");
            }
            var existingProduct = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Unit = product.Unit;
            existingProduct.BuyPrice = product.BuyPrice;
            existingProduct.SellPrice = product.SellPrice;
            existingProduct.Status = product.Status;
            existingProduct.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }
            var product = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }
            _context.Productos.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
