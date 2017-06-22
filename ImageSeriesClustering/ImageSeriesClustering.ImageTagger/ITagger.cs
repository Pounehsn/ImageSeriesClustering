using System.Threading.Tasks;

namespace ImageSeriesClustering.ImageTagger
{
    public interface ITagger
    {
        Task<string> Tag(string imageFilePath);
    }
}