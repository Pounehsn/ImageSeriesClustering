using System;
using System.Drawing;
using ImageProcessor;
using System.Collections.Generic;

namespace ImageSeriesClustering.Algorithem
{
    public class ImageComparer : IImageComparer
    {
        private readonly ImageFactory _factory = new ImageFactory();
        private readonly Size _size = new Size(256, 256);
        public double DifferenceScore(Image l, Image r)
        {
            if (l.Size != r.Size)
                throw new Exception($"Invalid arguments. Left and right images should have the same size.");

            var dif = 0.0;

            var bmpL = new Bitmap(l);
            var bmpR = new Bitmap(r);

            for (var x = 0; x < l.Width; x++)
                for (var y = 0; y < l.Height; y++)
                    dif += GetDifference(bmpL.GetPixel(x, y), bmpR.GetPixel(x, y));

            return dif / (l.Width * l.Height);
        }

        private Image MakeRegulareImage(Image image)
        {
            return _factory.Resize(_size).Image;
        }

        private IEnumerable<Image> SplitImage(Image image, Size partSize)
        {
            for (var x = 0; x < image.Width; x = +partSize.Width)
                for (var y = 0; y < image.Height; y += partSize.Height)
                {
                    yield return _factory.Crop(new Rectangle(new Point(x, y), partSize)).Image;
                }
        }

        private double GetDifference(Color lc, Color rc)
        {
            //var maxr = Math.Max(lc.R, rc.R);
            //var maxg = Math.Max(lc.G, rc.G);
            //var maxb = Math.Max(lc.B, rc.B);
            //return lc == rc
            //    ? 0
            //    : (
            //          (double)(maxr == 0 ? 0 : (double)Math.Abs(lc.R - rc.R) / maxr) +
            //          (double)(maxg == 0 ? 0 : (double)Math.Abs(lc.G - rc.G) / maxg) +
            //          (double)(maxb == 0 ? 0 : (double)Math.Abs(lc.B - rc.B) / maxb)
            //      ) / 3;

            var maxr = Math.Max(lc.R, rc.R);
            var maxg = Math.Max(lc.G, rc.G);
            var maxb = Math.Max(lc.B, rc.B);

            var maxAve = (maxr + maxg + maxb) / 3.0;

            var ave = (
                lc.R - rc.R +
                lc.G - rc.G +
                lc.B - rc.B +
                (255 * 3)
            ) / 6.0;

            return lc == rc
                ? 0
                : maxAve < double.Epsilon ? 0 : ave / maxAve;
        }
    }
}
