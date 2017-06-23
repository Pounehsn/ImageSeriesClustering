using System.Collections.Generic;
using System.IO;

namespace ImageSeriesClustering.Algorithem
{
    public class ProcessSeriesMessage
    {
        public ProcessSeriesMessage(IEnumerable<FileInfo> fileNames, FileInfo outputFile)
        {
            Files = fileNames;
            OutputFile = outputFile;
        }
        public IEnumerable<FileInfo> Files { get; }
        public FileInfo OutputFile { get; }
    }
}
