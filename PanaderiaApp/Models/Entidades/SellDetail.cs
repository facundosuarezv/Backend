using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class SellDetail
    {
        public int Id { get; set; }
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public Product Product { get; set; }
        public Sells Sell { get; set; }
    }
}
