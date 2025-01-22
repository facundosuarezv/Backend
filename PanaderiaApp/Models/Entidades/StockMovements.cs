using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class StockMovements
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

    public Products Producto { get; set; }
    }
}
