using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Domain
{
    interface ITopologyOperations
    {
        void ExchangeDeclare(Exchange exchange);
        void ExchangeDelete(Exchange exchange);

        void QueueDeclare(Queue queue);
        void QueueDelete(Queue queue);

        void QueueBind(Queue queue, Binding binding);
        void QueueUnbind(Queue queue, Binding binding);
    }
}
