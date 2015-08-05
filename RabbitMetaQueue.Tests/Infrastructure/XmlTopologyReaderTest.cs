using System.IO;
using NUnit.Framework;
using RabbitMetaQueue.Infrastructure;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue.Tests.Infrastructure
{
    [TestFixture]
    public class XmlTopologyReaderTest
    {
        [Test]
        public void BasicDefinition()
        {
            var topology = Parse("BasicDefinition.xml");
            Assert.AreEqual(2, topology.Exchanges.Count, "Exchange count");
            TestExchange(topology.Exchanges[0], "RMetaQ.test1", ExchangeType.Topic);
            TestExchange(topology.Exchanges[1], "RMetaQ.test2", ExchangeType.Topic);

            Assert.AreEqual(2, topology.Queues.Count, "Queue count");
            TestQueue(topology.Queues[0], "RMetaQ.queue1");
            TestQueue(topology.Queues[1], "RMetaQ.deadletter");

            var arguments = topology.Queues[0].Arguments;
            Assert.AreEqual(1, arguments.Count, "Queue Argument Count");
            Assert.IsTrue(arguments.ContainsKey("x-dead-letter-exchange"), "Dead Letter Exchange argument");
            Assert.AreEqual("metatest2", arguments["x-dead-letter-exchange"], "Dead Letter Exchange value");
        }


        private static void TestExchange(Exchange exchange, string expectedName, ExchangeType expectedType, bool expectedDurable = true)
        {
            Assert.AreEqual(expectedName, exchange.Name, "Exchange Name");
            Assert.AreEqual(expectedType, exchange.ExchangeType, "Exchange Type");
            Assert.AreEqual(expectedDurable, exchange.Durable, "Exchange Durable");
        }


        private static void TestQueue(Queue queue, string expectedName, bool expectedDurable = true)
        {
            Assert.AreEqual(expectedName, queue.Name, "Queue Name");
            Assert.AreEqual(expectedDurable, queue.Durable, "Queue Durable");
        }        
        
        
        private Topology Parse(string fileName)
        {
            using (var stream = GetType().Assembly.GetManifestResourceStream("RabbitMetaQueue.Tests.Data." + fileName))
            {
                return new XmlTopologyReader().Parse(stream);
            }
        }
    }
}
