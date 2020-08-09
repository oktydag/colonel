using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Core
{
    public class EventPublisher : IEventPublisher
    {
        public void Publish<T>(T @event) where T : IEvent
        {
           // Due to technology could be changed, I have given it to empty. Such as RabbitMQ, Kafka, Redis, In Memory etc...
        }
    }
}
