using System.Collections.Generic;
using System.Linq;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    class RabbitMQTopologyParser
    {
        private static readonly Dictionary<string, Model.ExchangeType> ExchangeTypeMap = new Dictionary<string, Model.ExchangeType>
        {
            { RabbitMQ.Client.ExchangeType.Fanout, Model.ExchangeType.Fanout },
            { RabbitMQ.Client.ExchangeType.Direct, Model.ExchangeType.Direct },
            { RabbitMQ.Client.ExchangeType.Topic, Model.ExchangeType.Topic },
            { RabbitMQ.Client.ExchangeType.Headers, Model.ExchangeType.Headers }
        };


        public Topology Parse(IManagementClient client, Vhost virtualHost)
        {
            var topology = new Topology();
            
            foreach (var exchange in client.GetExchanges())
            {
                var modelExchange = new Model.Exchange
                {
                    Name = exchange.Name,
                    ExchangeType = ExchangeTypeMap[exchange.Type],
                    Durable = exchange.Durable,
                };

                MapArguments(exchange.Arguments, modelExchange.Arguments);
                topology.Exchanges.Add(modelExchange);
            }

            foreach (var queue in client.GetQueues())
            {
                var modelQueue = new Model.Queue
                {
                    Name = queue.Name,
                    Durable = queue.Durable
                };

                MapArguments(queue.Arguments, modelQueue.Arguments);

                foreach (var binding in client.GetBindingsForQueue(queue))
                {
                    var modelBinding = new Model.Binding
                    {
                        Exchange = binding.Source,
                        RoutingKey = binding.RoutingKey
                    };

                    MapArguments(binding.Arguments, modelBinding.Arguments);
                    modelQueue.Bindings.Add(modelBinding);
                }

                topology.Queues.Add(modelQueue);
            }

            return topology;
        }


        private void MapArguments(Arguments arguments, List<Model.Argument> modelArguments)
        {
            modelArguments.AddRange(arguments.Select(argument => new Argument
            {
                Key = argument.Key, 
                Value = argument.Value
            }));
        }
    }
}
