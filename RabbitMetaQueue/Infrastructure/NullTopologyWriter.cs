using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    public class NullTopologyWriter : ITopologyWriter
    {
        public void CreateExchange(Exchange exchange)
        {
        }

        public void DeleteExchange(Exchange exchange)
        {
        }

        public void CreateQueue(Queue queue)
        {
        }

        public void DeleteQueue(Queue queue)
        {
        }

        public void CreateBinding(Queue queue, Binding binding)
        {
        }

        public void DeleteBinding(Queue queue, Binding binding)
        {
        }
    }
}
