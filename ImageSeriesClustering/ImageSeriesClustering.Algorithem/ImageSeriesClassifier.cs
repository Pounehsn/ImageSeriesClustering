using System.Collections.Generic;
using System.Drawing;
using ImageProcessor;

namespace ImageSeriesClustering.Algorithem
{
    public class ImageSeriesClassifier : IImageSeriesClassifier
    {
        public ImageSeriesClassifier(IImageComparer comparer, double threashold)
        {
            _comparer = comparer;
            _threashold = threashold;
        }

        private readonly IImageComparer _comparer;
        private readonly double _threashold;
        private readonly ImageFactory _factory = new ImageFactory();

        public IEnumerable<string> Classify(IEnumerable<string> series)
        {
            string fl = null;
            Image il = null;

            foreach (var file in series)
            {
                if (fl == null)
                {
                    fl = file;
                    il = _factory.Load(file).Image;
                }
                else
                {
                    var ir = _factory.Load(file).Image;

                    if (AreSimilar(il, ir)) continue;

                    yield return fl;
                    fl = file;
                    il = ir;
                }
            }

            yield return fl;
        }

        private bool AreSimilar(Image il, Image ir) => 
            _comparer.ImageSimilarityScore(il, ir) >= _threashold;
    }
}
