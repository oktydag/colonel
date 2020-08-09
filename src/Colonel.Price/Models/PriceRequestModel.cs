using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Price.Models
{
    public class PriceRequestModel
    {
        public int ProductId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
