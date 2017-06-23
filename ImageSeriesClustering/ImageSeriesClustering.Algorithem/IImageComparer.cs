using System.Drawing;

namespace ImageSeriesClustering.Algorithem
{
    public interface IImageComparer
    {
        double DifferenceScore(Image l, Image r);
    }
}