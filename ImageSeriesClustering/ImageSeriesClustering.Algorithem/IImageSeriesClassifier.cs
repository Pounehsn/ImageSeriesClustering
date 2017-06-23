using System.Collections.Generic;
using System.IO;

namespace ImageSeriesClustering.Algorithem
{
    public interface IImageSeriesClassifier
    {
        IEnumerable<FileInfo> Classify(IEnumerable<FileInfo> series);
    }
}