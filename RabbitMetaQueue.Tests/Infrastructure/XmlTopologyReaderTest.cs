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


        [Test]
        public void ArgumentTemplate()
        {
            var topology = Parse("ArgumentTemplate.xml");
            Assert.AreEqual(2, topology.Queues.Count, "Queue count");
            TestQueue(topology.Queues[0], "queue1");
            TestQueue(topology.Queues[1], "queue2");

            var arguments = topology.Queues[0].Arguments;
            Assert.AreEqual(1, arguments.Count, "Queue1 Argument Count");
            Assert.IsTrue(arguments.ContainsKey("x-dead-letter-exchange"), "Dead Letter Exchange argument");
            Assert.AreEqual("argh", arguments["x-dead-letter-exchange"], "Dead Letter Exchange value");

            arguments = topology.Queues[1].Arguments;
            Assert.AreEqual(2, arguments.Count, "Queue2 Argument Count");
            Assert.IsTrue(arguments.ContainsKey("x-dead-letter-exchange"), "Dead Letter Exchange argument");
            Assert.AreEqual("argh", arguments["x-dead-letter-exchange"], "Dead Letter Exchange value");
            Assert.IsTrue(arguments.ContainsKey("x-extend-template"), "Extra argument");
            Assert.AreEqual("test", arguments["x-extend-template"], "Extra value");
        }


        [Test]
        public void ArgumentTemplateInvalidName()
        {
            Assert.Catch(typeof(XmlTopologyReader.TemplateException), () => Parse("ArgumentTemplate-InvalidName.xml"));
        }


        [Test]
        public void ArgumentTemplateCircularReference()
        {
            Assert.Catch(typeof(XmlTopologyReader.TemplateException), () => Parse("ArgumentTemplate-CircularReference.xml"));
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
