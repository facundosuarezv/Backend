using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Usuarios { get; set; }
        public DbSet<Products> Productos { get; set; }
        public DbSet<Sells> Ventas { get; set; }
        public DbSet<SellDetails> DetalleVentas { get; set; }
        public DbSet<StockMovements> MovimientosStock { get; set; }

    }
}
