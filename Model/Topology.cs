using System.Collections.Generic;

namespace RabbitMetaQueue.Model
{
    class Topology
    {
        public List<Exchange> Exchanges { get; private set; }
        public List<Queue> Queues { get; private set; }


        public Topology()
        {
            Exchanges = new List<Exchange>();
            Queues = new List<Queue>();
        }
    }
}
