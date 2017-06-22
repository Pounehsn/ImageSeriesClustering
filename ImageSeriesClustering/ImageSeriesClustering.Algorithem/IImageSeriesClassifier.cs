using System.Collections.Generic;

namespace ImageSeriesClustering.Algorithem
{
    public interface IImageSeriesClassifier
    {
        IEnumerable<string> Classify(IEnumerable<string> series);
    }
}