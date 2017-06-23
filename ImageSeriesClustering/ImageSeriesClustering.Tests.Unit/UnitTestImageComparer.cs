using ImageSeriesClustering.Algorithem;
using NUnit.Framework;

namespace ImageSeriesClustering.Tests.Unit
{
    [TestFixture]
    public class UnitTestImageComparer
    {
        [Test]
        public void ImageSimilarityScore_SimilarImages_GraterThen52Percent()
        {
            var x = new ImageComparer();
            var similarity = x.DifferenceScore(Resource._20160815_050835_000, Resource._20160815_050922_000);
            Assert.IsTrue(similarity > 0.52);
        }
        [Test]
        public void ImageSimilarityScore_DifferentImages_LessThen52Percent()
        {
            var x = new ImageComparer();
            var similarity = x.DifferenceScore(Resource._20160815_050835_000, Resource._20160815_051539_000);
            Assert.IsTrue(similarity < 0.52);
        }
    }
}
