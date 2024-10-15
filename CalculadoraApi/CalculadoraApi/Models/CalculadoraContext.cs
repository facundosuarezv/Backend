using Microsoft.EntityFrameworkCore;

namespace CalculadoraApi.Models
{
    public class CalculadoraContext : DbContext
    {
        public CalculadoraContext(DbContextOptions<CalculadoraContext> options) : base(options) { }

        public DbSet<Calculo> Calculos { get; set; }
    }
}
