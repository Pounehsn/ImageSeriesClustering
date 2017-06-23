using System.IO;
using System.Linq;
using ImageSeriesClustering.Algorithem;
using NUnit.Framework;

namespace ImageSeriesClustering.Tests.Unit
{
    [TestFixture]
    public class UnitTestImageSeriesClassifier
    {
        [Test]
        public void Classify_ShouldPickOneOfEachCluster()
        {
            var clustrer = new ImageSeriesClassifier(
                new ImageComparer(), 
                0.04
            );

            var clusters = clustrer.Classify(
                new DirectoryInfo(
                    TestContext.CurrentContext.TestDirectory+
                    @"\..\..\Images"
                ).GetFiles()
            ).ToArray();

            Assert.AreEqual(12, clusters.Count());
        }
    }
}
