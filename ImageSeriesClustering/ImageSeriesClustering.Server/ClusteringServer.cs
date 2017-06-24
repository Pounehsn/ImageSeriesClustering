using System;
using System.Configuration;
using Akka.Actor;
using ImageSeriesClustering.Core;

namespace ImageSeriesClustering.Server
{
    public class ClusteringServer : IDisposable
    {
        private ActorSystem _actorSystem;

        public static void Main()
        {
            using (var server = new ClusteringServer())
            {
                server.Start();
                Console.ReadLine();
            }
        }

        public void Start()
        {
            var configManager = new ConfigFactory();

            var configurations = configManager.Create(
                ConfigurationManager.AppSettings.Get("hostname"),
                int.Parse(ConfigurationManager.AppSettings.Get("port"))
            );

            Console.WriteLine(configurations);

            _actorSystem = ActorSystem.Create(
                "ImageSeriesClusteringService", 
                configurations
            );

            _actorSystem.ActorOf<ClusteringActor>("clustering");
        }

        public void Dispose()
        {
            using (_actorSystem)
            {
                Console.WriteLine("Actor system is disposed.");
            }
        }
    }
}
