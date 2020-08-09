using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Stock.Models
{
    public class StockRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
