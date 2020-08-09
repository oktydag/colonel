using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Models
{
    public struct ProductAvailability
    {
        public ProductAvailability(string stockId, decimal price, int productId, bool isAvailable) : this()
        {
            StockId = stockId;
            Price = price;
            ProductId = productId;
            IsAvailable = isAvailable;
        }

        public string StockId { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public bool IsAvailable { get; set; }

        public static ProductAvailability NotAvailable
        {
            get
            {
                return new ProductAvailability(string.Empty, 0, 0, false);
            }
        }
    }
}
