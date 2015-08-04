using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace RabbitMetaQueue.Infrastructure
{
    class XmlTopologyReader
    {
        public Model.Topology Parse(string filename)
        {
            Schema.Topology definition;

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            { 
                var serializer = new XmlSerializer(typeof(Schema.Topology));
                definition = (Schema.Topology)serializer.Deserialize(stream);
            }
  
            var model = new Model.Topology();

            if (definition.Exchanges != null)
                MapExchanges(definition.Exchanges, model.Exchanges);

            if (definition.Queues != null)
                MapQueues(definition.Queues, model.Queues);

            return model;
        }


        private void MapExchanges(IEnumerable<Schema.Exchange> definition, List<Model.Exchange> model)
        {
            var exchangeTypeMap = new Dictionary<Schema.ExchangeType, Model.ExchangeType>
            {
                { Schema.ExchangeType.Fanout, Model.ExchangeType.Fanout },
                { Schema.ExchangeType.Direct, Model.ExchangeType.Direct },
                { Schema.ExchangeType.Topic, Model.ExchangeType.Topic },
                { Schema.ExchangeType.Headers, Model.ExchangeType.Headers },
            };


            foreach (var sourceExchange in definition)
            {
                var destExchange = new Model.Exchange
                {
                    Name = sourceExchange.name,
                    ExchangeType = exchangeTypeMap[sourceExchange.type],
                    Durable = sourceExchange.durable
                };

                if (sourceExchange.Arguments != null)
                    MapArguments(sourceExchange.Arguments, destExchange.Arguments);

                model.Add(destExchange);
            }
        }


        private void MapQueues(IEnumerable<Schema.Queue> definition, List<Model.Queue> model)
        {
            foreach (var sourceQueue in definition)
            {
                var destQueue = new Model.Queue
                {
                    Name = sourceQueue.name,
                    Durable = sourceQueue.durable
                };

                if (sourceQueue.Arguments != null)
                    MapArguments(sourceQueue.Arguments, destQueue.Arguments);

                if (sourceQueue.Bindings != null)
                    MapBindings(sourceQueue.Bindings, destQueue.Bindings);

                model.Add(destQueue);
            }            
        }


        private void MapBindings(IEnumerable<Schema.Binding> definition, List<Model.Binding> model)
        {
            foreach (var sourceBinding in definition)
            {
                var destBinding = new Model.Binding
                {
                    Exchange = sourceBinding.exchange,
                    RoutingKey = sourceBinding.routingKey
                };

                if (sourceBinding.Arguments != null)
                    MapArguments(sourceBinding.Arguments, destBinding.Arguments);

                model.Add(destBinding);
            }
        }


        private void MapArguments(IEnumerable<Schema.Argument> definition, List<Model.Argument> model)
        {
            model.AddRange(definition.Select(sourceArgument => new Model.Argument
            {
                Key = sourceArgument.name, 
                Value = sourceArgument.Value
            }));
        }
    }
}
