using System;
using System.IO;
using Akka.Actor;
using ImageSeriesClustering.Algorithem;

namespace ImageSeriesClustering.ConsoleApplication
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var imageFolder = @"D:\Pouneh\Image Series Clustering\ImageSeriesClustering\ImageSeriesClustering.ConsoleApplication\Images";
            var outDirectory = "../../OutPut";
            var cpuNumber = 14;
            // Create a new actor system (a container for your actors)
            var system = ActorSystem.Create("ImageClusteringSystem");

            var directoryInfo = new DirectoryInfo(imageFolder);
            var files = directoryInfo.GetFiles();
            var batchSize = files.Length / cpuNumber;
            // Send a message to the actor
            for (var i = 0; i < cpuNumber; i++)
            {
                // Create your actor and get a reference to it.
                // This will be an "ActorRef", which is not a
                // reference to the actual actor instance
                // but rather a client or proxy to it.
                var greeter = system.ActorOf<ClusteringActor>($"ImmageSeriesClustering{i}");

                var f = new FileInfo[batchSize];

                Array.Copy(files, i*batchSize, f, 0, batchSize);

                greeter.Tell(
                    new ProcessSeriesMessage(
                        f,
                        new FileInfo($"{outDirectory}\\{i}.txt")
                    )
                );
            }

            Console.WriteLine("Messages are sent.");

            // This prevents the application from exiting
            // before the asynchronous work is done
            Console.ReadLine();
        }
    }
}
