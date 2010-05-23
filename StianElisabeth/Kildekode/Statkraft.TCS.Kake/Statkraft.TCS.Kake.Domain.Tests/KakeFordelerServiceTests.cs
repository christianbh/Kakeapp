using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Statkraft.TCS.Kake.Domain.Tests
{
    [TestFixture]
    public class KakeFordelerServiceTests
    {
        private Mock<IEpostSender> _epostSenderMock;
        private KakeFordelerService _kakeFordelerService;
        private KakeFordeler _kakeFordeler;
        private DateTime _nextKakeDato;
        private KakeAnsvarlig _nextKakeAnsvarlig;
        private Mock<IKakeFordeler> _kakeFordelerMock;
        private Mock<IKakeFordelerRepository> _kakeFordelerRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _epostSenderMock = new Mock<IEpostSender>();
            _kakeFordelerRepositoryMock = new Mock<IKakeFordelerRepository>();
            _kakeFordelerService = new KakeFordelerService(_epostSenderMock.Object, _kakeFordelerRepositoryMock.Object);

            _nextKakeDato = new DateTime(2010, 04, 30);
            _nextKakeAnsvarlig = new KakeAnsvarlig("Roy", "roy@statkraft.com");
            _kakeFordeler = new KakeFordeler(null, null, _nextKakeAnsvarlig, _nextKakeDato, _nextKakeDato);

            _kakeFordelerRepositoryMock
                .Setup(m => m.GetFerieOversikt())
                .Returns(() => new FerieOversikt(new List<Ferie>()));

            _kakeFordelerRepositoryMock
                .Setup(m => m.GetMuligeKakeAnsvarlige())
                .Returns(() => new List<KakeAnsvarlig>() { new KakeAnsvarlig("", "") });
        }

        [Test]
        public void ShouldSendMail_WhenTimeIsWednesday1400InKakeUke()
        {
            DateTime wednesday1400 = new DateTime(2010, 04, 28, 14, 00, 00);
            SetUpEpostSenderMock();

            _kakeFordelerService.Run(_kakeFordeler, wednesday1400);

            _epostSenderMock.Verify();
        }

        [Test]
        public void ShouldSendMail_WhenTimeIsWednesday1430InKakeUke()
        {
            DateTime wednesday1430 = new DateTime(2010, 04, 28, 14, 30, 00);
            SetUpEpostSenderMock();

            _kakeFordelerService.Run(_kakeFordeler, wednesday1430);

            _epostSenderMock.Verify(
                x => x.Send(It.IsAny<KakeAnsvarlig>(), It.IsAny<DateTime>()), 
                Times.Never());
        }

        [Test]
        public void ShouldUpdateNextKakeAnsvarligAndKakeDato_WhenFriday1400InKakeUke()
        {
            DateTime friday1400 = new DateTime(2010, 04, 30, 14, 00, 00);
            SetUpKakeFordelerMock();
            SetupKakeFordelerRepositoryMockForSaving();

            _kakeFordelerService.Run(_kakeFordelerMock.Object, friday1400);

            _kakeFordelerMock.Verify();
            _kakeFordelerRepositoryMock.Verify();
        }

        [Test]
        public void ShouldNotUpdateNextKakeAnsvarligAndKakeDato_WhenFriday1430InKakeUke()
        {
            DateTime friday1430 = new DateTime(2010, 04, 30, 14, 30, 00);
            SetUpKakeFordelerMock();

            _kakeFordelerService.Run(_kakeFordelerMock.Object, friday1430);

            _kakeFordelerMock
                .Verify(m => m.FindNextKakeAnsvarlig(),
                Times.Never());
        }

        [Test]
        public void ShouldInitializeAndGetKakeFordelerFromRepository()
        {
            _kakeFordelerRepositoryMock
                .Setup(m => m.GetNextKakeAnsvarlig())
                .Returns(() => new KakeAnsvarlig("", ""));

            _kakeFordelerRepositoryMock
                .Setup(m => m.GetNextKakeDato())
                .Returns(() => DateTime.Now);

            KakeFordeler kakeFordeler = _kakeFordelerService.Initialize();

            Assert.That(kakeFordeler, Is.Not.Null);
            Assert.That(kakeFordeler.FerieOversikt, Is.Not.Null);
            Assert.That(kakeFordeler.MuligeKakeAnsvarlige, Is.Not.Null);
            Assert.That(kakeFordeler.NextKakeAnsvarlig, Is.Not.Null);
            Assert.That(kakeFordeler.NextKakeDato, Is.Not.Null);
        }

        [Test]
        public void ShouldInitializeAndFindNewKakeAnsvarlig_WhenNoExistingNextKakeAnsvarlig()
        {
            _kakeFordelerRepositoryMock
                .Setup(m => m.GetNextKakeAnsvarlig())
                .Returns(() => null);

            _kakeFordelerRepositoryMock
                .Setup(m => m.GetNextKakeDato())
                .Returns(() => DateTime.Now);

            KakeFordeler kakeFordeler = _kakeFordelerService.Initialize();

            Assert.That(kakeFordeler.NextKakeAnsvarlig, Is.Not.Null);
        }

        [Test]
        public void ShouldInitializeAndSaveNewKakeAnsvarligAndNextKakeDato()
        {
            _kakeFordelerRepositoryMock
                .Setup(m => m.GetNextKakeAnsvarlig())
                .Returns(() => null);

            _kakeFordelerRepositoryMock
                .Setup(m => m.GetNextKakeDato())
                .Returns(() => DateTime.Now);

            SetupKakeFordelerRepositoryMockForSaving();

            _kakeFordelerService.Initialize();

            _kakeFordelerRepositoryMock.VerifyAll();
        }

        private void SetupKakeFordelerRepositoryMockForSaving()
        {
            _kakeFordelerRepositoryMock
                .Setup(m => m.SaveNextKakeAnsvarlig(It.IsAny<KakeAnsvarlig>()))
                .Verifiable();

            _kakeFordelerRepositoryMock
                .Setup(m => m.SaveNextKakeDato(It.IsAny<DateTime>()))
                .Verifiable();
        }

        private void SetUpEpostSenderMock()
        {
            _epostSenderMock
                .Setup(m => m.Send(_nextKakeAnsvarlig, _nextKakeDato))
                .Verifiable();
        }

        private void SetUpKakeFordelerMock()
        {
            _kakeFordelerMock = new Mock<IKakeFordeler>();

            _kakeFordelerMock
                .SetupGet(m => m.NextKakeAnsvarlig)
                .Returns(_nextKakeAnsvarlig);

            _kakeFordelerMock
                .SetupGet(m => m.NextKakeDato)
                .Returns(_nextKakeDato);

            _kakeFordelerMock
                .Setup(m => m.FindNextKakeAnsvarlig())
                .Verifiable();

            _kakeFordelerMock
                .Setup(m => m.FindNextKakeDato(It.IsAny<DateTime>()))
                .Verifiable();
        }
    }
}
