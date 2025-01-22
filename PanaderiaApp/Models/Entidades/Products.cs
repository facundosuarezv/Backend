using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public decimal BuyPrice { get; set; } 
        public decimal SellPrice { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
