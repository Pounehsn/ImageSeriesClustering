using System;
using System.Threading;
using Akka.Actor;

namespace ImageSeriesClustering.Algorithem
{
    public class ClusteringActor : ReceiveActor
    {
        public ClusteringActor()
        {
            Receive((Action<ProcessSeriesMessage>)Handler);
        }

        private void Handler(ProcessSeriesMessage message)
        {
            Console.WriteLine(
                $"Processing from '{message.StaringFileName}', {message.Count} images."
            );

            for (var i = 0; i < message.Count; i++)
            {
                Console.WriteLine($"{i} '{message.StaringFileName}' processed");
                Thread.Sleep(1000);
            }
        }
    }
}