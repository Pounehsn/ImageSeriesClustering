using System.Drawing;

namespace ImageSeriesClustering.Algorithem
{
    public interface IImageComparer
    {
        double ImageSimilarityScore(Image l, Image r);
    }
}