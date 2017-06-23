using System;
using Akka.Actor;

namespace ImageSeriesClustering.Algorithem
{
    public class ClusteringActor : ReceiveActor
    {
        public ClusteringActor()
        {
            Receive((Action<ProcessSeriesMessage>)Handler);
        }

        readonly ImageSeriesClassifier _clustrer = 
            new ImageSeriesClassifier(new ImageComparer(), 0.04);

        private void Handler(ProcessSeriesMessage message)
        {
            using (var stream = message.OutputFile.CreateText())
            {
                foreach (var item in _clustrer.Classify(message.Files))
                {
                    stream.Write(item.FullName);
                }
                stream.Flush();
            }
        }
    }
}