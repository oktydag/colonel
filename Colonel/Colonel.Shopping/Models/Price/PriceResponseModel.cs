using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Models.Price
{
    public class PriceResponseModel
    {
        public int ProductId { get; set; }

        public decimal Value { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool IsActive { get; set; }
    }
}
