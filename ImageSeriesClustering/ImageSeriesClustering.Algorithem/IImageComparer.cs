namespace ImageSeriesClustering.Algorithem
{
    public interface IImageComparer
    {
        double ImageSimilarityScore(string l, string r);
    }
}