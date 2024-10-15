using CalculadoraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

[Route("api/[Controller]")]
[ApiController]
public class CalculoController : ControllerBase
{
    private readonly CalculadoraContext _context;

    public CalculoController (CalculadoraContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Calculo>> PostCalculo(Calculo calculo)
    {
        var resultado = calculo.Resultado;

        _context.Calculos.Add(calculo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCalculos), new {id = calculo.Id}, calculo);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Calculo>>> GetCalculos()
    {
        return await _context.Calculos.ToListAsync();
    }
}
