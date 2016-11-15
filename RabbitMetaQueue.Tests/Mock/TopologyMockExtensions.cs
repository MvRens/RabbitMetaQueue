using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Tests.Mock
{
    public static class TopologyMockExtensions
    {
        public static Exchange AddExchange1(this Topology topology, string prefix = "")
        {
            var exchange = new Exchange
            {
                Name = prefix + "e1",
                ExchangeType = ExchangeType.Topic
            };

            topology.Exchanges.Add(exchange);
            return exchange;
        }

    
        public static Queue AddQueue1(this Topology topology, string prefix = "")
        {
            var queue = new Queue
            {
                Name = prefix + "q1"
            };

            topology.Queues.Add(queue);
            return queue;
        }


        public static Binding BindToExchange1(this Queue queue, string routingKey, string prefix = "")
        {
            var binding = new Binding()
            {
                Exchange = prefix + "e1",
                RoutingKey = routingKey
            };

            queue.Bindings.Add(binding);
            return binding;
        }
    }
}
