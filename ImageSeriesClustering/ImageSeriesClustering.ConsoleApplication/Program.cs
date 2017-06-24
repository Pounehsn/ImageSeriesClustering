using System;
using System.IO;
using Akka.Actor;
using ImageSeriesClustering.Core;
using System.Configuration;
using System.Linq;
using ImageSeriesClustering.ImageTagger;

namespace ImageSeriesClustering.ConsoleApplication
{
    public static class Program
    {
        private static void Main(params string[] args)
        {

            var imageFolder = args[0];
            var outDirectory = args[1];

            var factory = new ConfigFactory();

            var configurations = factory.Create(
                ConfigurationManager.AppSettings.Get("hostname"),
                int.Parse(ConfigurationManager.AppSettings.Get("port"))
            );

            Console.WriteLine(configurations);


            var directoryInfo = new DirectoryInfo(imageFolder);
            var directories = directoryInfo.GetDirectories();
            var processorNumber = Math.Min(Environment.ProcessorCount, directories.Length);
            var dirPerCpu = directories.Length / processorNumber;
            var dirsForACpu = directories
                .Select((v, i) => new { value = v, index = i })
                .GroupBy(i => i.index / dirPerCpu)
                .Select(i => i.Select(j => j.value).ToArray())
                .ToArray();

            // Create a new actor system (a container for your actors)
            using (
                var system = ActorSystem.Create(
                    "ImageSeriesClusteringService",
                    configurations
                )
            )
            {

                // Send a message to the actor
                for (var i = 0; i < processorNumber; i++)
                {
                    // Create your actor and get a reference to it.
                    // This will be an "ActorRef", which is not a
                    // reference to the actual actor instance
                    // but rather a client or proxy to it.

                    //get a reference to the remote actor
                    var host = ConfigurationManager.AppSettings.Get("serverhostname");
                    var port = int.Parse(ConfigurationManager.AppSettings.Get("serverport"));
                    var actor = system
                        .ActorSelection(
                            $"akka.tcp://ImageSeriesClusteringService@{host}:{port}/user/clustering"
                        );

                    //send a message to the remote actor
                    actor.Tell(
                         new ProcessSeriesMessage(
                             dirsForACpu[i].SelectMany(d => d.GetFiles()),
                             new FileInfo($"{outDirectory}\\{i}.txt")
                         )
                     );
                }
            }

            Console.WriteLine("Messages are sent.");

            // This prevents the application from exiting
            // before the asynchronous work is done
            Console.ReadLine();

            var resultDir = new FileInfo($"{outDirectory}\\FinalList.txt");
            using (var resultStream = resultDir.CreateText())
            {
                for (var i = 0; i < processorNumber; i++)
                {
                    var partialResultFile = new FileInfo($"{outDirectory}\\{i}.txt");

                    using (var partialResultStream = partialResultFile.OpenText())
                    {
                        resultStream.WriteLine(partialResultStream.ReadToEnd());
                    }
                }
            }

            var tagger = new Tagger("'feaf4fc6b0c6421fbdb1c44acf166901'", "westus.api.cognitive.microsoft.com");

            var tagFile = new FileInfo($"{outDirectory}\\MicrosoftVisionAPI.txt");

            using (var resultStream = resultDir.OpenText())
            {
                using (var tagStream = tagFile.CreateText())
                {
                    while (!resultStream.EndOfStream)
                    {
                        var file = resultStream.ReadLine();

                        var tags = tagger.Tag(file).Result;

                        tagStream.WriteLine(tags);
                    }
                }
            }
        }
    }
}
