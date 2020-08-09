using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Models
{
    public class AddProductToBasketRequestModel
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public string GiftNote { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
