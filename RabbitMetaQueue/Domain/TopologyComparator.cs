using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMetaQueue.Model;
using RabbitMetaQueue.Resources;
using Serilog;

namespace RabbitMetaQueue.Domain
{
    public class TopologyComparator
    {
        public bool AllowDelete { get; set; }
        public bool AllowRecreate { get; set; }
        public bool AllowUnbind { get; set; }

        private readonly ILogger logger;
        private readonly ITopologyWriter topologyWriter;
        private readonly List<string> volatileExchanges = new List<string>();


        public TopologyComparator(ILogger logger, ITopologyWriter topologyWriter)
        {
            AllowDelete = false;
            AllowRecreate = false;
            AllowUnbind = false;

            this.logger = logger;
            this.topologyWriter = topologyWriter;
        }


        public void Compare(Topology existingTopology, Topology definedTopology)
        {
            volatileExchanges.Clear();

            logger.Debug(Strings.LogCheckAddUpdateExchanges);
            foreach (var exchange in definedTopology.Exchanges)
            {
                var existingExchange = existingTopology.Exchanges.FirstOrDefault(e => e.Name.Equals(exchange.Name, StringComparison.InvariantCulture));
                if (existingExchange != null)
                    UpdateExchange(exchange, existingExchange);
                else
                    CreateExchange(exchange);
            }

            if (AllowDelete)
            {
                logger.Debug(Strings.LogCheckRemovedExchanges);
                foreach (var exchange in existingTopology.Exchanges.Except(definedTopology.Exchanges, new ExchangeComparer()))
                    DeleteExchange(exchange);
            }
            else
                logger.Debug(Strings.LogCheckRemovedExchangesSkipped);

            logger.Debug(Strings.LogCheckAddUpdateQueues);
            foreach (var queue in definedTopology.Queues)
            {
                var existingQueue = existingTopology.Queues.FirstOrDefault(q => q.Name.Equals(queue.Name, StringComparison.InvariantCulture));
                if (existingQueue != null)
                    UpdateQueue(queue, existingQueue);
                else
                    CreateQueue(queue);
            }

            if (AllowDelete)
            {
                logger.Debug(Strings.LogCheckRemovedQueues);
                foreach (var queue in existingTopology.Queues.Except(definedTopology.Queues, new QueueComparer()))
                    DeleteQueue(queue);
            }
            else
                logger.Debug(Strings.LogCheckRemovedQueuesSkipped);
        }

        private void CreateExchange(Exchange exchange)
        {
            logger.Information(Strings.LogCreateExchange, exchange.Name, exchange.ExchangeType);
            topologyWriter.CreateExchange(exchange);
        }


        private void UpdateExchange(Exchange exchange, Exchange existingExchange)
        {
            if (SameExchange(exchange, existingExchange))
            {
                logger.Debug(Strings.LogExchangeUnchanged, exchange.Name);
                return;
            }

            if (!AllowRecreate)
            {
                logger.Debug(Strings.LogExchangeNoRecreate, exchange.Name);
                return;
            }

            logger.Information(Strings.LogExchangeRecreate, exchange.Name, exchange.ExchangeType);
            topologyWriter.DeleteExchange(existingExchange);
            topologyWriter.CreateExchange(exchange);

            // Bindings need to be recreated as well
            volatileExchanges.Add(exchange.Name);
        }


        private void DeleteExchange(Exchange exchange)
        {
            logger.Information(Strings.LogDeleteExchange, exchange.Name);
            topologyWriter.DeleteExchange(exchange);
        }


        private void CreateQueue(Queue queue)
        {
            logger.Information(Strings.LogCreateQueue, queue.Name);
            topologyWriter.CreateQueue(queue);
            CreateQueueBindings(queue);
        }


        private void UpdateQueue(Queue queue, Queue existingQueue)
        {
            if (SameQueue(queue, existingQueue))
            {
                logger.Debug(Strings.LogQueueUnchanged, queue.Name);
                UpdateQueueBindings(queue, existingQueue);
                return;
            }

            if (AllowRecreate)
            {
                logger.Information(Strings.LogQueueRecreate, queue.Name);
                topologyWriter.DeleteQueue(queue);
                topologyWriter.CreateQueue(queue);
                CreateQueueBindings(queue);
            }
            else
            {
                logger.Debug(Strings.LogQueueNoRecreate, queue.Name);
                UpdateQueueBindings(queue, existingQueue);
            }
        }


        private void DeleteQueue(Queue queue)
        {
            logger.Information(Strings.LogDeleteQueue, queue.Name);
            topologyWriter.DeleteQueue(queue);
        }


        private void CreateQueueBindings(Queue queue)
        {
            foreach (var binding in queue.Bindings)
                CreateBinding(queue, binding);            
        }


        private void UpdateQueueBindings(Queue queue, Queue existingQueue)
        {
            logger.Debug(Strings.LogCheckAddUpdateBindings, queue.Name);
            foreach (var binding in queue.Bindings)
            {
                var existingBinding = existingQueue.Bindings.FirstOrDefault(b => b.Exchange.Equals(binding.Exchange, StringComparison.InvariantCulture) &&
                                                                                    b.RoutingKey.Equals(binding.RoutingKey, StringComparison.InvariantCulture));
                if (existingBinding != null)
                    UpdateBinding(queue, binding, existingBinding);
                else
                    CreateBinding(queue, binding);
            }

            if (AllowDelete)
            {
                logger.Debug(Strings.LogCheckRemovedBindings);
                foreach (var binding in existingQueue.Bindings.Except(queue.Bindings, new BindingComparer()))
                    DeleteBinding(existingQueue, binding);
            }
            else
                logger.Debug(Strings.LogCheckRemovedBindingsSkipped);
        }


        private void CreateBinding(Queue queue, Binding binding)
        {
            logger.Information(Strings.LogCreateBinding, queue.Name, binding.Exchange, binding.RoutingKey);

            topologyWriter.CreateBinding(queue, binding);
        }


        private void UpdateBinding(Queue queue, Binding binding, Binding existingBinding)
        {
            if (volatileExchanges.Contains(binding.Exchange, StringComparer.InvariantCulture))
            {
                logger.Information(Strings.LogBindRecreatedExchange, binding.Exchange, queue.Name, binding.RoutingKey);
                topologyWriter.CreateBinding(queue, binding);
                return;
            }

            if (SameBinding(binding, existingBinding))
            {
                logger.Debug(Strings.LogBindingUnchanged, queue.Name, binding.Exchange, binding.RoutingKey);
                return;
            }

            if (!AllowRecreate)
            {
                logger.Debug(Strings.LogBindingNoRecreate, queue.Name, binding.Exchange, binding.RoutingKey);
                return;
            }

            logger.Information(Strings.LogBindingRecreate, queue.Name, binding.Exchange, binding.RoutingKey);

            topologyWriter.DeleteBinding(queue, existingBinding);
            CreateBinding(queue, binding);
        }


        private void DeleteBinding(Queue queue, Binding binding)
        {
            logger.Information(Strings.LogDeleteBinding, queue.Name, binding.Exchange, binding.RoutingKey);
            topologyWriter.DeleteBinding(queue, binding);
        }

        
        private static bool SameExchange(Exchange exchange, Exchange existingExchange)
        {
            return (exchange.Durable == existingExchange.Durable) && 
                   SameArguments(exchange.Arguments, existingExchange.Arguments);
        }


        private static bool SameQueue(Queue queue, Queue existingQueue)
        {
            return (queue.Durable == existingQueue.Durable) &&
                   SameArguments(queue.Arguments, existingQueue.Arguments);
        }


        private static bool SameBinding(Binding binding, Binding existingBinding)
        {
            return SameArguments(binding.Arguments, existingBinding.Arguments);
        }


        private static bool SameArguments(Arguments arguments, Arguments existingArguments)
        {
            if (arguments.Count != existingArguments.Count)
                return false;

            string value;
            return arguments.All(a => existingArguments.TryGetValue(a.Key, out value) && 
                                      value.Equals(a.Value, StringComparison.InvariantCulture));
        }
    }


    class ExchangeComparer : IEqualityComparer<Exchange>
    {
        public bool Equals(Exchange x, Exchange y)
        {
            return (string.Compare(x.Name, y.Name, StringComparison.Ordinal) == 0);
        }

        public int GetHashCode(Exchange obj)
        {
            return obj.Name.GetHashCode();
        }
    }


    class QueueComparer : IEqualityComparer<Queue>
    {
        public bool Equals(Queue x, Queue y)
        {
            return (string.Compare(x.Name, y.Name, StringComparison.Ordinal) == 0);
        }

        public int GetHashCode(Queue obj)
        {
            return obj.Name.GetHashCode();
        }
    }


    class BindingComparer : IEqualityComparer<Binding>
    {
        public bool Equals(Binding x, Binding y)
        {
            return (string.Compare(x.Exchange, y.Exchange, StringComparison.Ordinal) == 0) &&
                   (string.Compare(x.RoutingKey, y.RoutingKey, StringComparison.Ordinal) == 0);
        }

        public int GetHashCode(Binding obj)
        {
            return (obj.Exchange + obj.RoutingKey).GetHashCode();
        }
    }
}
