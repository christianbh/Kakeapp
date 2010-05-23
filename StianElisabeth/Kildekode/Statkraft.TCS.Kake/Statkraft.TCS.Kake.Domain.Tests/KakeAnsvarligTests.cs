using NUnit.Framework;

namespace Statkraft.TCS.Kake.Domain.Tests
{
    [TestFixture]
    public class KakeAnsvarligTests
    {

        [Test]
        public void ShouldToString()
        {
            KakeAnsvarlig kakeAnsvarlig = new KakeAnsvarlig("Christian", "christian@statkraft.no");
            string kakeAnsvarligString = kakeAnsvarlig.ToString();

            Assert.That(kakeAnsvarligString.Contains("Christian"));
            Assert.That(kakeAnsvarligString.Contains("christian@statkraft.no"));
        }

        [Test]
        public void ShouldReturnTrueForTwoEqualKakeAnsvarlige()
        {
            KakeAnsvarlig kakeAnsvarlig1 = new KakeAnsvarlig("Christian", "christian@statkraft.no");
            KakeAnsvarlig kakeAnsvarlig2 = new KakeAnsvarlig("Christian", "christian@statkraft.no");

            Assert.That(kakeAnsvarlig1, Is.EqualTo(kakeAnsvarlig2));
        }
    }
}
