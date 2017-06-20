using System;
using Akka.Actor;
using ImageSeriesClustering.Algorithem;

namespace ImageSeriesClustering.ConsoleApplication
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            // Create a new actor system (a container for your actors)
            var system = ActorSystem.Create("ImageClusteringSystem");

            // Send a message to the actor
            for (var i = 0; i < 3; i++)
            {
                // Create your actor and get a reference to it.
                // This will be an "ActorRef", which is not a
                // reference to the actual actor instance
                // but rather a client or proxy to it.
                var greeter = system.ActorOf<ClusteringActor>($"ImmageSeriesClustering{i}");

                greeter.Tell(new ProcessSeriesMessage($"File{i}", 100));
            }

            Console.WriteLine("Messages are sent.");

            // This prevents the application from exiting
            // before the asynchronous work is done
            Console.ReadLine();
        }
    }
}
