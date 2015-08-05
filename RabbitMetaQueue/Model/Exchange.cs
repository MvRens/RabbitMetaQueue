namespace RabbitMetaQueue.Model
{
    public enum ExchangeType
    {
        Fanout,
        Direct,
        Topic, 
        Headers
    }

    public class Exchange
    {
        public string Name { get; set; }
        public ExchangeType ExchangeType { get; set; }
        public bool Durable { get; set; }
        public Arguments Arguments { get; private set; }


        public Exchange()
        {
            ExchangeType = ExchangeType.Direct;
            Durable = true;
            Arguments = new Arguments();
        }
    }
}