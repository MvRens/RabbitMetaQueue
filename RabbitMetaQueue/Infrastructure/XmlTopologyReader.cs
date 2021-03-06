﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using RabbitMetaQueue.Resources;
using RabbitMetaQueue.Schema;

namespace RabbitMetaQueue.Infrastructure
{
    public class XmlTopologyReader
    {
        public class TemplateException : Exception
        {
            public TemplateException(string message) : base(message) { }
        }


        private Topology definition;


        public Model.Topology Parse(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            { 
                return Parse(stream);
            }
          }


        public Model.Topology Parse(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Topology));
            
            definition = (Topology)serializer.Deserialize(stream);
            var model = new Model.Topology();

            if (definition.Settings != null)
                model.Meta.NamespacePrefix = definition.Settings.Namespace;

            if (definition.Exchanges != null)
                MapExchanges(definition.Exchanges, model.Exchanges);

            if (definition.Queues != null)
                MapQueues(definition.Queues, model.Queues);

            return model;
        }


        private void MapExchanges(IEnumerable<Exchange> exchanges, List<Model.Exchange> model)
        {
            var exchangeTypeMap = new Dictionary<ExchangeType, Model.ExchangeType>
            {
                { ExchangeType.Fanout, Model.ExchangeType.Fanout },
                { ExchangeType.Direct, Model.ExchangeType.Direct },
                { ExchangeType.Topic, Model.ExchangeType.Topic },
                { ExchangeType.Headers, Model.ExchangeType.Headers },
            };


            foreach (var sourceExchange in exchanges)
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


        private void MapQueues(IEnumerable<Queue> queues, List<Model.Queue> model)
        {
            foreach (var sourceQueue in queues)
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


        private void MapBindings(IEnumerable<Binding> bindings, List<Model.Binding> model)
        {
            foreach (var sourceBinding in bindings)
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


        private void MapArguments(Arguments arguments, Model.Arguments model)
        {
            MapArguments(arguments, model, new HashSet<string>());
        }


        private void MapArguments(Arguments arguments, Model.Arguments model, HashSet<string> stackTrace)
        {
            if (!string.IsNullOrEmpty(arguments.template))
            {
                if (stackTrace.Contains(arguments.template, StringComparer.InvariantCulture))
                    throw new TemplateException(string.Format(Strings.XmlTemplateCircularReference, arguments.template));
                
                var template = GetTemplate(arguments.template, TemplateType.Arguments);

                stackTrace.Add(arguments.template);
                MapArguments(template.Item, model, stackTrace);
            }

            if (arguments.Argument != null)
            { 
                foreach (var argument in arguments.Argument)
                    model.Add(argument.name, argument.Value);
            }
        }


        // ReSharper disable once UnusedParameter.Local
        private Template GetTemplate(string name, TemplateType type)
        {
            if (definition.Templates == null)
                throw new TemplateException(Strings.XmlNoTemplatesElement);

            var template = definition.Templates.FirstOrDefault(t => t.name.Equals(name, StringComparison.InvariantCulture));
            if (template == null)
                throw new TemplateException(string.Format(Strings.XmlTemplateNotFound, name));

            if (template.type != type)
                throw new TemplateException(string.Format(Strings.XmlTemplateUnexpectedType, name));

            return template;
        }
    }
}
