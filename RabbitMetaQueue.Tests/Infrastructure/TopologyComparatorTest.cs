using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Model;
using RabbitMetaQueue.Tests.Mock;

namespace RabbitMetaQueue.Tests.Infrastructure
{
    [TestFixture]
    public class TopologyComparatorTest
    {
        private Topology definedTopology;
        private Topology existingTopology;

        [SetUp]
        public void SetUp()
        {
            definedTopology = new Topology();
            existingTopology = new Topology();
        }


        [Test]
        public void CreateExchange()
        {
            definedTopology.AddExchange1();

            TestCompare(new List<string>
            {
                { "ce:e1:Topic:True" }
            });
        }


        [Test]
        public void CreateQueue()
        {
            definedTopology.AddQueue1();

            TestCompare(new List<string>
            {
                { "cq:q1:True" }
            });            
        }


        [Test]
        public void CreateBinding()
        {
            definedTopology.AddExchange1();
            definedTopology
                .AddQueue1()
                .BindToExchange1("mock.key");

            TestCompare(new List<string>
            {
                { "ce:e1:Topic:True" },
                { "cq:q1:True" },
                { "cb:q1:e1:mock.key" }
            });         
        }


        [Test]
        public void DeleteBinding()
        {
            definedTopology.AddExchange1();
            definedTopology.AddQueue1();

            existingTopology.AddExchange1();
            existingTopology
                .AddQueue1()
                .BindToExchange1("mock.key");

            TestCompare(new List<string>
            {
                { "db:q1:e1:mock.key" }
            });
        }


        [Test]
        public void NothingChanged()
        {
            definedTopology.AddExchange1();
            definedTopology.AddQueue1();

            existingTopology.AddExchange1();
            existingTopology.AddQueue1();

            TestCompare(new List<string>());
        }


        [Test]
        public void RemoveExchange()
        {
            existingTopology.AddExchange1();

            TestCompare(new List<string>
            {
                { "de:e1" }
            });
        }


        [Test]
        public void RemoveQueue()
        {
            existingTopology.AddQueue1();

            TestCompare(new List<string>
            {
                { "dq:q1" }
            });
        }

        private void TestCompare(List<string> expectedActions)
        {
            var writer = new MockTopologyWriter();
            var comparator = new TopologyComparator(writer)
            {
                AllowDelete = true,
                AllowRecreate = true,
                AllowUnbind = true
            };

            comparator.Compare(existingTopology, definedTopology);

            var hasUnexpectedActions = false;
            var results = new StringBuilder();

            results.AppendLine("Expected actions not executed:");
            foreach (var line in expectedActions.Except(writer.Actions))
            {
                results.AppendLine("  " + line);
                hasUnexpectedActions = true;
            }

            results.AppendLine("Executed action not expected:");
            foreach (var line in writer.Actions.Except(expectedActions))
            {
                results.AppendLine("  " + line);
                hasUnexpectedActions = true;
            }

            if (hasUnexpectedActions)
            {
                results.AppendLine("Full log:");
                foreach (var line in writer.Actions)
                    results.AppendLine("  " + line);

                Assert.Fail(results.ToString());
            }
        }
    }


    class MockTopologyWriter : ITopologyWriter
    {
        public List<string> Actions { get; private set; } 


        public MockTopologyWriter()
        {
            Actions = new List<string>();
        }


        public void CreateExchange(Exchange exchange)
        {
            Actions.Add("ce:" + exchange.Name + ":" + exchange.ExchangeType + ":" + exchange.Durable);
        }

        public void DeleteExchange(Exchange exchange)
        {
            Actions.Add("de:" + exchange.Name);
        }

        public void CreateQueue(Queue queue)
        {
            Actions.Add("cq:" + queue.Name + ":" + queue.Durable);
        }

        public void DeleteQueue(Queue queue)
        {
            Actions.Add("dq:" + queue.Name);
        }

        public void CreateBinding(Queue queue, Binding binding)
        {
            Actions.Add("cb:" + queue.Name + ":" + binding.Exchange + ":" + binding.RoutingKey);
        }

        public void DeleteBinding(Queue queue, Binding binding)
        {
            Actions.Add("db:" + queue.Name + ":" + binding.Exchange + ":" + binding.RoutingKey);
        }
    }
}
