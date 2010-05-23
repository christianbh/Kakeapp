using System;
using System.Collections.Generic;
using System.Threading;
using Moq;
using NUnit.Framework;
using Statkraft.TCS.Kake.Domain;

namespace Statkraft.TCS.Kake.Tests
{
    [TestFixture]
    public class KakeAppTests
    {
        [Test]
        public void ShouldInitializeOnStartup()
        {
            Mock<IKakeFordelerService> kakeFordelerServiceMock = new Mock<IKakeFordelerService>();

            kakeFordelerServiceMock
                .Setup(m => m.Initialize())
                .Returns(() => new KakeFordeler(new List<KakeAnsvarlig>{new KakeAnsvarlig("","")}, null, null, DateTime.Now, DateTime.Now))
                .Verifiable();

            new KakeApp(60, kakeFordelerServiceMock.Object);

            kakeFordelerServiceMock.VerifyAll();
        }

        [Test] 
        public void ShouldFireRegularly()
        {
            Mock<IKakeFordelerService> kakeFordelerServiceMock = new Mock<IKakeFordelerService>();

            kakeFordelerServiceMock
                .Setup(m => m.Run(It.IsAny<KakeFordeler>(), It.IsAny<DateTime>()))
                .Verifiable();

            new KakeApp(1, kakeFordelerServiceMock.Object);
            
            Thread.Sleep(2500);

            kakeFordelerServiceMock
                .Verify(m => m.Run(It.IsAny<KakeFordeler>(), It.IsAny<DateTime>()),
                Times.AtLeast(2));
        }
    }
}
