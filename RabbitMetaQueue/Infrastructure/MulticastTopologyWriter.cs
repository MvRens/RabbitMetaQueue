using System.Collections.Generic;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    public class MulticastTopologyWriter : ITopologyWriter
    {
        private readonly List<ITopologyWriter> topologyWriters = new List<ITopologyWriter>();

        public void Add(ITopologyWriter topologyWriter)
        {
            topologyWriters.Add(topologyWriter);
        }


        public void CreateExchange(Exchange exchange)
        {
            topologyWriters.ForEach(to => to.CreateExchange(exchange));
        }

        public void DeleteExchange(Exchange exchange)
        {
            topologyWriters.ForEach(to => to.DeleteExchange(exchange));
        }

        public void CreateQueue(Queue queue)
        {
            topologyWriters.ForEach(to => to.CreateQueue(queue));
        }

        public void DeleteQueue(Queue queue)
        {
            topologyWriters.ForEach(to => to.DeleteQueue(queue));
        }

        public void CreateBinding(Queue queue, Binding binding)
        {
            topologyWriters.ForEach(to => to.CreateBinding(queue, binding));
        }

        public void DeleteBinding(Queue queue, Binding binding)
        {
            topologyWriters.ForEach(to => to.DeleteBinding(queue, binding));
        }
    }
}
