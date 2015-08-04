namespace RabbitMetaQueue.Model
{
    class Binding
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public Arguments Arguments { get; set; }


        public Binding()
        {
            Arguments = new Arguments();
        }
    }
}
