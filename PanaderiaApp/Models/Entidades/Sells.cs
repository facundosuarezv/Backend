using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Sells
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        public List<SellDetails> Detalles { get; set; }
    }
}
