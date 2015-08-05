using System.Collections.Generic;

namespace RabbitMetaQueue.Model
{
    public class Queue
    {
        public string Name { get; set; }
        public bool Durable { get; set; }
        public Arguments Arguments { get; private set; }
        public List<Binding> Bindings { get; private set; }

        public Queue()
        {
            Durable = true;
            Arguments = new Arguments();
            Bindings = new List<Binding>();
        }
    }
}
