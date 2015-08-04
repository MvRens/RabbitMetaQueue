using System.Collections.Generic;

namespace RabbitMetaQueue.Model
{
    class Binding
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public List<Argument> Arguments { get; set; }


        public Binding()
        {
            Arguments = new List<Argument>();
        }
    }
}
