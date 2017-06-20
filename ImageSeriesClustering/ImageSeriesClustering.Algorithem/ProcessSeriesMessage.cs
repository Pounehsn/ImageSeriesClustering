namespace ImageSeriesClustering.Algorithem
{
    public class ProcessSeriesMessage
    {
        public ProcessSeriesMessage(string staringFileName, int count)
        {
            StaringFileName = staringFileName;
            Count = count;
        }
        public string StaringFileName { get; }
        public int Count { get; }
    }
}
