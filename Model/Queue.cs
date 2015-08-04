using System.Collections.Generic;

namespace RabbitMetaQueue.Model
{
    class Queue
    {
        public string Name { get; set; }
        public bool Durable { get; set; }
        public List<Argument> Arguments { get; private set; }
        public List<Binding> Bindings { get; private set; }

        public Queue()
        {
            Arguments = new List<Argument>();
            Bindings = new List<Binding>();
        }
    }
}
