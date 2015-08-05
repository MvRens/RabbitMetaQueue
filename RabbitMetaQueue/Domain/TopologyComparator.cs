using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMetaQueue.Model;
using RabbitMQ.Client.Framing.Impl;

namespace RabbitMetaQueue.Domain
{
    public class TopologyComparator
    {
        public bool AllowDelete { get; set; }
        public bool AllowRecreate { get; set; }
        public bool AllowUnbind { get; set; }

        private readonly ITopologyWriter topologyWriter;
        private readonly List<string> volatileExchanges = new List<string>();


        public TopologyComparator()
        {
            AllowDelete = false;
            AllowRecreate = false;
            AllowUnbind = false;
        }


        public TopologyComparator(ITopologyWriter topologyWriter)
        {
            this.topologyWriter = topologyWriter;
        }


        public void Compare(Topology existingTopology, Topology definedTopology)
        {
            volatileExchanges.Clear();

            // Added or updated exchanges
            foreach (var exchange in definedTopology.Exchanges)
            {
                var existingExchange = existingTopology.Exchanges.FirstOrDefault(e => e.Name.Equals(exchange.Name, StringComparison.InvariantCulture));
                if (existingExchange != null)
                    UpdateExchange(exchange, existingExchange);
                else
                    CreateExchange(exchange);
            }

            // Removed exchanges
            foreach (var exchange in existingTopology.Exchanges.Except(definedTopology.Exchanges, new ExchangeComparer()))
                DeleteExchange(exchange);

            // Added or updated queues
            foreach (var queue in definedTopology.Queues)
            {
                var existingQueue = existingTopology.Queues.FirstOrDefault(q => q.Name.Equals(queue.Name, StringComparison.InvariantCulture));
                if (existingQueue != null)
                    UpdateQueue(queue, existingQueue);
                else
                    CreateQueue(queue);
            }

            // Removed queues
            foreach (var queue in existingTopology.Queues.Except(definedTopology.Queues, new QueueComparer()))
                DeleteQueue(queue);
        }

        private void CreateExchange(Exchange exchange)
        {
            topologyWriter.CreateExchange(exchange);
        }


        private void UpdateExchange(Exchange exchange, Exchange existingExchange)
        {
            if (AllowRecreate && !SameExchange(exchange, existingExchange))
            { 
                topologyWriter.DeleteExchange(existingExchange);
                topologyWriter.CreateExchange(exchange);

                // Bindings need to be recreated as well
                volatileExchanges.Add(exchange.Name);
            }
        }


        private void DeleteExchange(Exchange exchange)
        {
            if (AllowDelete)
                topologyWriter.DeleteExchange(exchange);
        }


        private void CreateQueue(Queue queue)
        {
            topologyWriter.CreateQueue(queue);

            foreach (var binding in queue.Bindings)
                CreateBinding(queue, binding);
        }


        private void UpdateQueue(Queue queue, Queue existingQueue)
        {
            if (AllowRecreate && !SameQueue(queue, existingQueue))
            {
                topologyWriter.DeleteQueue(existingQueue);
                CreateQueue(queue);
            }
            else
            {
                foreach (var binding in queue.Bindings)
                {
                    var existingBinding = existingQueue.Bindings.FirstOrDefault(b => b.Exchange.Equals(binding.Exchange, StringComparison.InvariantCulture) &&
                                                                                     b.RoutingKey.Equals(binding.RoutingKey, StringComparison.InvariantCulture));
                    if (existingBinding != null)
                        UpdateBinding(queue, binding, existingBinding);
                    else
                        CreateBinding(queue, binding);
                }

                foreach (var binding in existingQueue.Bindings.Except(queue.Bindings, new BindingComparer()))
                    DeleteBinding(existingQueue, binding);
            }
        }


        private void DeleteQueue(Queue queue)
        {
            if (AllowDelete)
                topologyWriter.DeleteQueue(queue);
        }


        private void CreateBinding(Queue queue, Binding binding)
        {
            topologyWriter.CreateBinding(queue, binding);
        }


        private void UpdateBinding(Queue queue, Binding binding, Binding existingBinding)
        {
            if (volatileExchanges.Contains(binding.Exchange, StringComparer.InvariantCulture))
            {
                CreateBinding(queue, binding);
            }
            else if (AllowRecreate && !SameBinding(binding, existingBinding))
            {
                topologyWriter.DeleteBinding(queue, existingBinding);
                CreateBinding(queue, binding);
            }
        }


        private void DeleteBinding(Queue queue, Binding binding)
        {
            if (AllowDelete)
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
            return (String.Compare(x.Name, y.Name, StringComparison.Ordinal) == 0);
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
            return (String.Compare(x.Name, y.Name, StringComparison.Ordinal) == 0);
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
            return (String.Compare(x.Exchange, y.Exchange, StringComparison.Ordinal) == 0) &&
                   (String.Compare(x.RoutingKey, y.RoutingKey, StringComparison.Ordinal) == 0);
        }

        public int GetHashCode(Binding obj)
        {
            return (obj.Exchange + obj.RoutingKey).GetHashCode();
        }
    }
}
