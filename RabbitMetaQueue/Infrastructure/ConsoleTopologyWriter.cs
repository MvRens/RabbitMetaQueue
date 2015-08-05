using System;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Infrastructure
{
    // ToDo arguments
    public class ConsoleTopologyWriter : ITopologyWriter
    {
        public void CreateExchange(Exchange exchange)
        {
            Console.WriteLine("> Adding exchange: " + exchange.Name);
            Console.WriteLine("    Type: " + exchange.ExchangeType);
            Console.WriteLine("    Durable: " + exchange.Durable);
        }


        public void DeleteExchange(Exchange exchange)
        {
            Console.WriteLine("> Deleting exchange: " + exchange.Name);
        }


        public void CreateQueue(Queue queue)
        {
            Console.WriteLine("> Adding queue: " + queue.Name);
            Console.WriteLine("    Durable: " + queue.Durable);
        }


        public void DeleteQueue(Queue queue)
        {
            Console.WriteLine("> Deleting queue: " + queue.Name);
        }


        public void CreateBinding(Queue queue, Binding binding)
        {
            Console.WriteLine("> Binding queue: " + queue.Name);
            Console.WriteLine("    Exchange: " + binding.Exchange);
            Console.WriteLine("    Routing key: " + binding.RoutingKey);
        }


        public void DeleteBinding(Queue queue, Binding binding)
        {
            Console.WriteLine("> Unbinding queue: " + queue.Name);
            Console.WriteLine("    Exchange: " + binding.Exchange);
            Console.WriteLine("    Routing key: " + binding.RoutingKey);
        }
    }
}
