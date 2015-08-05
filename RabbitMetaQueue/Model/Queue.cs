using System.Collections.Generic;

namespace RabbitMetaQueue.Model
{
    class Queue
    {
        public string Name { get; set; }
        public bool Durable { get; set; }
        public Arguments Arguments { get; private set; }
        public List<Binding> Bindings { get; private set; }

        public Queue()
        {
            Arguments = new Arguments();
            Bindings = new List<Binding>();
        }
    }
}
