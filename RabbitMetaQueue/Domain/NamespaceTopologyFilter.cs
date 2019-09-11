using System;
using System.Linq;
using RabbitMetaQueue.Model;
using RabbitMetaQueue.Resources;
using Serilog;

namespace RabbitMetaQueue.Domain
{
    public static class NamespaceTopologyFilter
    {
        public static Topology FilterByNamespace(this Topology topology, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return topology;

            return new Topology(
                topology.Meta,
                topology.Exchanges.Where(e => Matches(e.Name, prefix)).ToList(),
                topology.Queues.Where(q => Matches(q.Name, prefix)).ToList()
            );
        }


        public static bool VerifyNamespaces(this Topology topology, ILogger logger, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return true;

            var queues = topology.Queues.Where(q => !Matches(q.Name, prefix)).ToList();
            if (queues.Count <= 0) 
                return true;

            logger.Error(Strings.LogInvalidNamespacesQueue, prefix, queues.Select(q => q.Name).ToArray() );
            return false;
        }


        private static bool Matches(string name, string prefix)
        {
            return name.Equals(prefix, StringComparison.InvariantCultureIgnoreCase) ||
                   name.StartsWith(prefix + ".", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
