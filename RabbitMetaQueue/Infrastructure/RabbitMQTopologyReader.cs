using System;
using System.Collections.Generic;
using System.Linq;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    public class RabbitMQTopologyReader
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
            
            foreach (var exchange in client.GetExchanges().Where(e => e.Vhost == virtualHost.Name))
            {
                if (!IsSystemExchange(exchange.Name))
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
            }

            foreach (var queue in client.GetQueues().Where(q => q.Vhost == virtualHost.Name))
            {
                if (!IsSystemQueue(queue))
                { 
                    var modelQueue = new Model.Queue
                    {
                        Name = queue.Name,
                        Durable = queue.Durable
                    };

                    MapArguments(queue.Arguments, modelQueue.Arguments);

                    foreach (var binding in client.GetBindingsForQueue(queue))
                    {
                        if (!IsSystemBinding(binding))
                        {
                            var modelBinding = new Model.Binding
                            {
                                Exchange = binding.Source,
                                RoutingKey = binding.RoutingKey
                            };

                            MapArguments(binding.Arguments, modelBinding.Arguments);
                            modelQueue.Bindings.Add(modelBinding);
                        }
                    }

                    topology.Queues.Add(modelQueue);
                }
            }

            return topology;
        }


        private void MapArguments(EasyNetQ.Management.Client.Model.Arguments arguments, Model.Arguments modelArguments)
        {
            foreach (var argument in arguments)
                modelArguments.Add(argument.Key, argument.Value);
        }


        private static bool IsSystemExchange(string name)
        {
            return (string.IsNullOrEmpty(name) ||
                    name.StartsWith("amq.", StringComparison.InvariantCulture));
        }


        private static bool IsSystemQueue(EasyNetQ.Management.Client.Model.Queue queue)
        {
            return (queue.AutoDelete ||
                    string.IsNullOrEmpty(queue.Name) ||
                    queue.Name.StartsWith("amq."));
        }


        private static bool IsSystemBinding(EasyNetQ.Management.Client.Model.Binding binding)
        {
            return (string.IsNullOrEmpty(binding.Source));
        }
    }
}
