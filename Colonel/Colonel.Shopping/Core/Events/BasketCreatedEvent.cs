using Colonel.Shopping.Entities;
using System.Collections.Generic;

namespace Colonel.Shopping.Core.Events
{
    public class BasketCreatedEvent: IEvent
    {
        public string BasketId { get; set; }
        public List<BasketLine> BasketLines { get; set; }
        public int UserId { get; set; }
    }
}
