using System.Collections.Generic;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    class MulticastTopologyOperations : ITopologyOperations
    {
        private readonly List<ITopologyOperations> topologyOperationsList = new List<ITopologyOperations>();

        public void Add(ITopologyOperations topologyOperations)
        {
            topologyOperationsList.Add(topologyOperations);
        }


        public void ExchangeDeclare(Exchange exchange)
        {
            topologyOperationsList.ForEach(to => to.ExchangeDeclare(exchange));
        }

        public void ExchangeDelete(Exchange exchange)
        {
            topologyOperationsList.ForEach(to => to.ExchangeDelete(exchange));
        }

        public void QueueDeclare(Queue queue)
        {
            topologyOperationsList.ForEach(to => to.QueueDeclare(queue));
        }

        public void QueueDelete(Queue queue)
        {
            topologyOperationsList.ForEach(to => to.QueueDelete(queue));
        }

        public void QueueBind(Queue queue, Binding binding)
        {
            topologyOperationsList.ForEach(to => to.QueueBind(queue, binding));
        }

        public void QueueUnbind(Queue queue, Binding binding)
        {
            topologyOperationsList.ForEach(to => to.QueueUnbind(queue, binding));
        }
    }
}
