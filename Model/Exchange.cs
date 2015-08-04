namespace RabbitMetaQueue.Model
{
    public enum ExchangeType
    {
        Fanout,
        Direct,
        Topic, 
        Headers
    }

    class Exchange
    {
        public string Name { get; set; }
        public ExchangeType ExchangeType { get; set; }
        public bool Durable { get; set; }
        public Arguments Arguments { get; private set; }


        public Exchange()
        {
            Arguments = new Arguments();
        }
    }
}