namespace RabbitMetaQueue.Model
{
    public class ConnectionParams
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ConnectionParams()
        {
            Host = "localhost";
            VirtualHost = "/";
            Username = "guest";
            Password = "guest";
        }
    }
}
