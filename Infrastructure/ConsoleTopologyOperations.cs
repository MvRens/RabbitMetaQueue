using System;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    // ToDo arguments
    class ConsoleTopologyOperations : ITopologyOperations
    {
        public void ExchangeDeclare(Exchange exchange)
        {
            Console.WriteLine("> Adding exchange: " + exchange.Name);
            Console.WriteLine("    Type: " + exchange.ExchangeType);
            Console.WriteLine("    Durable: " + exchange.Durable);
        }


        public void ExchangeDelete(Exchange exchange)
        {
            Console.WriteLine("> Deleting exchange: " + exchange.Name);
        }


        public void QueueDeclare(Queue queue)
        {
            Console.WriteLine("> Adding queue: " + queue.Name);
            Console.WriteLine("    Durable: " + queue.Durable);
        }


        public void QueueDelete(Queue queue)
        {
            Console.WriteLine("> Deleting queue: " + queue.Name);
        }


        public void QueueBind(Queue queue, Binding binding)
        {
            Console.WriteLine("> Binding queue: " + queue.Name);
            Console.WriteLine("    Exchange: " + binding.Exchange);
            Console.WriteLine("    Routing key: " + binding.RoutingKey);
        }


        public void QueueUnbind(Queue queue, Binding binding)
        {
            Console.WriteLine("> Unbinding queue: " + queue.Name);
            Console.WriteLine("    Exchange: " + binding.Exchange);
            Console.WriteLine("    Routing key: " + binding.RoutingKey);
        }
    }
}
