using System.Collections.Generic;

namespace RabbitMetaQueue.Model
{
    public class Topology
    {
        public TopologyMeta Meta { get; private set; }
        public List<Exchange> Exchanges { get; private set; }
        public List<Queue> Queues { get; private set; }


        public Topology()
        {
            Exchanges = new List<Exchange>();
            Queues = new List<Queue>();
            Meta = new TopologyMeta();
        }


        public Topology(TopologyMeta meta, List<Exchange> exchanges, List<Queue> queues)
        {
            Meta = meta;
            Exchanges = exchanges;
            Queues = queues;
        }
    }


    public class TopologyMeta
    {
        public string NamespacePrefix { get; set; }
    }
}
