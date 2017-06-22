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
        public double ImageSimilarityScore(Image l, Image r)
        {
            if (l.Size != r.Size)
                throw new Exception($"Invalid arguments. Left and right images should have the same size.");

            var dif = 0.0;

            var bmpL = new Bitmap(l);
            var bmpR = new Bitmap(r);

            for (var x = 0; x < l.Width; x++)
                for (var y = 0; y < l.Height; y++)
                    dif += GetDifference(bmpL.GetPixel(x, y), bmpR.GetPixel(x, y));

            return 1 - dif / l.Width * l.Height;
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

        private double GetDifference(Color lc, Color rc) =>
            lc == rc
                ? 0
                : lc.R - rc.R / Math.Max(lc.R, rc.R) +
                  lc.G - rc.G / Math.Max(lc.G, rc.G) +
                  lc.B - rc.B / Math.Max(lc.B, rc.B);
    }
}
