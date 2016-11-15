using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EasyNetQ.Management.Client;
using NDesk.Options;
using RabbitMetaQueue.Domain;
using RabbitMetaQueue.Infrastructure;
using RabbitMetaQueue.Model;
using RabbitMetaQueue.Resources;
using Serilog;

namespace RabbitMetaQueue
{
    class Program
    {
        private class Options
        {
            public string TopologyFilename { get; set; }
            public bool DryRun { get; set; }
            public bool Mirror { get; set; }
            public bool Verbose { get; set; }

            public ConnectionParams ConnectionParams { get; }

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

                var logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

                var optionTable = new TextTable();
                optionTable.Add(Strings.OptionDryRun, options.DryRun ? Strings.OptionDryRunYes
                                                                     : Strings.OptionDryRunNo);
                optionTable.Add(Strings.OptionMirror, options.Mirror ? Strings.OptionYes
                                                                     : Strings.OptionNo);
                optionTable.Add(Strings.OptionVerbose, options.Verbose ? Strings.OptionYes
                                                                       : Strings.OptionNo);

                Console.WriteLine(Strings.Options);
                Console.Write(optionTable.ToString());
                Console.WriteLine();

                try
                { 
                    Console.WriteLine(Strings.StatusParsingDefinition);
                    var definedTopology = new XmlTopologyReader().Parse(options.TopologyFilename);

                    Console.WriteLine(Strings.StatusConnectingRabbitMQ, options.ConnectionParams.Host, options.ConnectionParams.VirtualHost);
                    var client = Connect(options.ConnectionParams);
                    var virtualHost = client.GetVhost(options.ConnectionParams.VirtualHost);

                    Console.WriteLine(Strings.StatusReadingTopology);
                    var existingTopology = new RabbitMQTopologyReader().Parse(client, virtualHost);

                    var writer = (options.DryRun ? (ITopologyWriter)new NullTopologyWriter() 
                                                 : new RabbitMQTopologyWriter(client, virtualHost));

                    var comparator = new TopologyComparator(logger, writer)
                    {
                        AllowDelete = options.Mirror,
                        AllowRecreate = options.Mirror,
                        AllowUnbind = options.Mirror
                    };
                        
                    Console.WriteLine(Strings.StatusComparing);
                    comparator.Compare(existingTopology, definedTopology);

                    Console.WriteLine();
                    Console.WriteLine(Strings.StatusDone);
                    return 0;
                }
                catch(Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(Strings.StatusError, e.Message);
                    return 1;
                }            
            }
            finally
            {
                if (Debugger.IsAttached)
                {
                    Console.WriteLine();
                    Console.WriteLine(Strings.StatusWaitForKey);
                    Console.ReadLine();
                }
            }
        }


        private static IManagementClient Connect(ConnectionParams connectionParams)
        {
            return new ManagementClient($"http://{connectionParams.Host}",
                                        connectionParams.Username,
                                        connectionParams.Password);
        }


        private static bool ParseOptions(IEnumerable<string> args, Options options)
        {
            string filename = null;

            var optionSet = new OptionSet
            {
                {
                    Strings.OptionKeyInput, Strings.OptionDescriptionInput,
                    v => filename = v
                },
                {
                    Strings.OptionKeyHost, Strings.OptionDescriptionHost,
                    v => options.ConnectionParams.Host = v
                },
                {
                    Strings.OptionKeyVirtualHost, Strings.OptionDescriptionVirtualHost,
                    v => options.ConnectionParams.VirtualHost = v
                },
                {
                    Strings.OptionKeyUsername, Strings.OptionDescriptionUsername,
                    v => options.ConnectionParams.Username = v
                },
                {
                    Strings.OptionKeyPassword, Strings.OptionDescriptionPassword,
                    v => options.ConnectionParams.Password = v
                },
                {
                    Strings.OptionKeyDryRun, Strings.OptionDescriptionDryRun,
                    v => options.DryRun = (v != null)
                },
                {
                    Strings.OptionKeyMirror, Strings.OptionDescriptionMirror,
                    v => options.Mirror = (v != null)
                },
                {
                    Strings.OptionKeyVerbose, Strings.OptionDescriptionVerbose,
                    v => options.Verbose = (v != null)
                }
            };            

            try
            {
                optionSet.Parse(args);

                if (string.IsNullOrEmpty(filename))
                    throw new OptionException(Strings.OptionInputRequired, Strings.OptionKeyInput);

                if (!File.Exists(filename))
                    throw new OptionException(Strings.OptionInputNotFound, Strings.OptionKeyInput);

                options.TopologyFilename = filename;
                return true;
            }
            catch(OptionException e)
            { 
                Console.Write(Strings.OptionInvalidArguments);
                Console.WriteLine(e.Message);

                Console.WriteLine(Strings.OptionUsage);
                optionSet.WriteOptionDescriptions(Console.Out);

                return false;
            }
        }
    }
}
