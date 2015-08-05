using System.Collections.Generic;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using RabbitMetaQueue.Domain;

namespace RabbitMetaQueue.Infrastructure
{
    class RabbitMQTopologyWriter : ITopologyWriter
    {
        private readonly IManagementClient client;
        private readonly Vhost virtualHost;

        private static readonly Dictionary<Model.ExchangeType, string> ExchangeTypeMap = new Dictionary<Model.ExchangeType, string>
        {
            { Model.ExchangeType.Fanout, RabbitMQ.Client.ExchangeType.Fanout },
            { Model.ExchangeType.Direct, RabbitMQ.Client.ExchangeType.Direct },
            { Model.ExchangeType.Topic, RabbitMQ.Client.ExchangeType.Topic },
            { Model.ExchangeType.Headers, RabbitMQ.Client.ExchangeType.Headers }
        };


        public RabbitMQTopologyWriter(IManagementClient client, Vhost virtualHost)
        {
            this.client = client;
            this.virtualHost = virtualHost;
        }


        public void CreateExchange(Model.Exchange exchange)
        {
            client.CreateExchange(new ExchangeInfo(exchange.Name, ExchangeTypeMap[exchange.ExchangeType], false, exchange.Durable, false, 
                                                   MapArguments(exchange.Arguments)), virtualHost);
        }


        public void DeleteExchange(Model.Exchange exchange)
        {
            client.DeleteExchange(client.GetExchange(exchange.Name, virtualHost));
        }


        public void CreateQueue(Model.Queue queue)
        {
            client.CreateQueue(new QueueInfo(queue.Name, false, queue.Durable, MapInputArguments(queue.Arguments)), virtualHost);
        }


        public void DeleteQueue(Model.Queue queue)
        {
            client.DeleteQueue(client.GetQueue(queue.Name, virtualHost));
        }


        public void CreateBinding(Model.Queue queue, Model.Binding binding)
        {
            client.CreateBinding(client.GetExchange(binding.Exchange, virtualHost),
                                 client.GetQueue(queue.Name, virtualHost),
                                 new BindingInfo(binding.RoutingKey, MapInputArguments(binding.Arguments)));
        }


        public void DeleteBinding(Model.Queue queue, Model.Binding binding)
        {
            foreach (var clientBinding in client.GetBindingsForQueue(client.GetQueue(queue.Name, virtualHost)))
            {
                if (clientBinding.Source.Equals(binding.Exchange) &&
                    clientBinding.RoutingKey.Equals(binding.RoutingKey))
                {
                    client.DeleteBinding(clientBinding);
                }
            }
        }


        public Arguments MapArguments(Model.Arguments arguments)
        {
            var mapping = new Arguments();
            foreach (var argument in arguments)
                mapping.Add(argument.Key, argument.Value);

            return mapping;
        }


        public InputArguments MapInputArguments(Model.Arguments arguments)
        {
            var mapping = new InputArguments();
            foreach (var argument in arguments)
                mapping.Add(argument.Key, argument.Value);

            return mapping;
        }
    }
}
