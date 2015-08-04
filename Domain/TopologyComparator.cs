using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Domain
{
    class TopologyComparator
    {
        public bool AllowDelete { get; set; }
        public bool AllowRecreate { get; set; }
        public bool AllowUnbind { get; set; }

        private ITopologyOperations topologyOperations;


        public TopologyComparator()
        {
            AllowDelete = false;
            AllowRecreate = false;
            AllowUnbind = false;
        }


        public TopologyComparator(ITopologyOperations topologyOperations)
        {
            this.topologyOperations = topologyOperations;
        }


        public void Compare(Topology existingTopology, Topology definedTopology)
        {
            // ToDo Compare implementation
        }
    }
}
