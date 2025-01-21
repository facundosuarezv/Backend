using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Sells
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }

        public List<SellDetail> Detalles { get; set; }
    }
}
