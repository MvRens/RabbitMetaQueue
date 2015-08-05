using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EasyNetQ.Management.Client;
using NDesk.Options;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Infrastructure;
using RabbitMetaQueue.Model;

namespace RabbitMetaQueue
{
    class Program
    {
        private class Options
        {
            public string TopologyFilename { get; set; }
            public bool DryRun { get; set; }
            public ConnectionParams ConnectionParams { get; private set; }

            public Options()
            {
                ConnectionParams = new ConnectionParams();
                DryRun = false;
            }
        }

        static int Main(string[] args)
        {
            try
            {
                var options = new Options();
                if (!ParseOptions(args, options))
                    return 1;

                try
                { 
                    Console.WriteLine("Parsing topology definition");
                    var definedTopology = new XmlTopologyReader().Parse(options.TopologyFilename);

                    Console.WriteLine("Connecting to RabbitMQ server [{0}{1}]", options.ConnectionParams.Host, options.ConnectionParams.VirtualHost);
                    var client = Connect(options.ConnectionParams);
                    var virtualHost = client.GetVhost(options.ConnectionParams.VirtualHost);

                    Console.WriteLine("Reading existing topology");
                    var existingTopology = new RabbitMQTopologyReader().Parse(client, virtualHost);

                    var writer = new MulticastTopologyWriter();
                    writer.Add(new ConsoleTopologyWriter());

                    if (!options.DryRun)
                    { 
                        Console.WriteLine("Changes WILL be applied");
                        writer.Add(new RabbitMQTopologyWriter(client, virtualHost));
                    }
                    else
                        Console.WriteLine("Dry run - changes will not be applied");

                    var comparator = new TopologyComparator(writer)
                    {
                        AllowDelete = true,
                        AllowRecreate = true,
                        AllowUnbind = true
                    };
                        
                    Console.WriteLine("Comparing topology");
                    comparator.Compare(existingTopology, definedTopology);

                    Console.WriteLine("Done!");
                    return 0;
                }
                catch(Exception e)
                {
                    Console.Write("Error: ");
                    Console.WriteLine(e.Message);
                    return 1;
                }            
            }
            finally
            {
                if (Debugger.IsAttached)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any Enter key to continue...");
                    Console.ReadLine();
                }
            }
        }


        private static IManagementClient Connect(ConnectionParams connectionParams)
        {
            return new ManagementClient(String.Format("http://{0}", connectionParams.Host),
                                        connectionParams.Username,
                                        connectionParams.Password);
        }


        private static bool ParseOptions(IEnumerable<string> args, Options options)
        {
            string filename = null;

            var optionSet = new OptionSet
            {
                {
                    "i|input=", "The {file name} of the topology definition. Required.",
                    v => filename = v
                },
                {
                    "h|host=", "The host {name} of the RabbitMQ server. Defaults to localhost.", 
                    v => options.ConnectionParams.Host = v
                },
                {
                    "v|virtualhost=", "The virtual host {name} as configured in RabbitMQ. Defaults to /.",
                    v => options.ConnectionParams.VirtualHost = v
                },
                {
                    "u|username=", "The {username} used in the connection. Defaults to guest.",
                    v => options.ConnectionParams.Username = v
                },
                {
                    "p|password=", "The {password} used in the connection. Defaults to guest.",
                    v => options.ConnectionParams.Password = v
                },
                {
                    "d|dryrun", "When specified, changes are not applied to the RabbitMQ server.",
                    v => options.DryRun = (v != null)
                }
            };            

            try
            {
                optionSet.Parse(args);

                if (String.IsNullOrEmpty(filename))
                    throw new OptionException("Topology file name is required", "i");

                if (!File.Exists(filename))
                    throw new OptionException("Topology file not found", "i");

                options.TopologyFilename = filename;
                return true;
            }
            catch(OptionException e)
            { 
                Console.Write("Invalid arguments: ");
                Console.WriteLine(e.Message);

                Console.WriteLine("Usage:");
                optionSet.WriteOptionDescriptions(Console.Out);

                return false;
            }
        }
    }
}
