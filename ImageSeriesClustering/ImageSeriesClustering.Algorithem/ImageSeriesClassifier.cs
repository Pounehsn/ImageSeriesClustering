using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

        //public IEnumerable<FileInfo> Classify(IEnumerable<FileInfo> series)
        //{
        //    FileInfo fl = null;
        //    Image il = null;

        //    foreach (var file in series)
        //    {
        //        if (fl == null)
        //        {
        //            fl = file;
        //            il = _factory.Load(file.FullName).Image;
        //        }
        //        else
        //        {
        //            var ir = _factory.Load(file.FullName).Image;

        //            if (AreSimilar(il, ir)) continue;

        //            yield return fl;
        //            fl = file;
        //            il = ir;
        //        }
        //    }

        //    yield return fl;
        //}

        //public IEnumerable<FileInfo> Classify(IEnumerable<FileInfo> series)
        //{
        //    var files = series.ToArray();

        //    if (files.Length < 1) yield break;

        //    var ir = _factory.Load(files[0].FullName).Image;
        //    yield return files[0];

        //    for (var i = 1; i < files.Length - 1; i++)
        //    {
        //        var il = ir;
        //        ir = _factory.Load(files[i + 1].FullName).Image;

        //        if (AreSimilar(il, ir)) continue;

        //        yield return files[i + 1];
        //    }
        //}

        public IEnumerable<FileInfo> Classify(IEnumerable<FileInfo> series)
        {
            var files = series.ToArray();
            var firsts = true;
            var a2 = 0.0;
            var i = 0;
            foreach (var similarity in SimilaritySeries(files))
            {
                var a1 = a2;
                a2 = similarity;

                if (firsts)
                {
                    firsts = false;
                    continue;
                }

                if (Math.Abs(a1 - a2) > _threashold)
                    yield return files[i];

                i++;
            }
        }

        private IEnumerable<double> SimilaritySeries(FileInfo[] files)
        {
            if (files.Length < 1) yield break;

            var ir = _factory.Load(files[0].FullName).Image;

            for (var i = 1; i < files.Length - 1; i++)
            {
                var il = ir;
                ir = _factory.Load(files[i + 1].FullName).Image;
                
                yield return _comparer.DifferenceScore(il, ir);
            }
        }

        private bool AreSimilar(Image il, Image ir)
        {
            var similarity = _comparer.DifferenceScore(il, ir);
            return similarity >= _threashold;
        }
    }
}
