using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Domain
{
    interface ITopologyWriter
    {
        void CreateExchange(Exchange exchange);
        void DeleteExchange(Exchange exchange);

        void CreateQueue(Queue queue);
        void DeleteQueue(Queue queue);

        void CreateBinding(Queue queue, Binding binding);
        void DeleteBinding(Queue queue, Binding binding);
    }
}
