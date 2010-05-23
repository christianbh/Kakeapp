using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Statkraft.TCS.Kake.Domain.Tests
{
    [TestFixture]
    public class KakeFordelerTests
    {
        private IList<KakeAnsvarlig> _kakeAnsvarlige;
        private KakeFordeler _kakeFordeler;
        private Mock<IFerieOversikt> _ferieOversiktMock;

        [SetUp]
        public void SetUp()
        {
            _kakeAnsvarlige = CreateKakeAnsvarligeList();
            _ferieOversiktMock = new Mock<IFerieOversikt>();
        }

        [Test]
        public void ShouldFindNextKakeAnsvarlig_WhenNoPreviousKakeAnsvarligExists()
        {
            KakeAnsvarlig previousKakeAnsvarlig = null;
            _kakeFordeler = new KakeFordeler(_kakeAnsvarlige, _ferieOversiktMock.Object, previousKakeAnsvarlig, DateTime.Now, DateTime.Now);

            KakeAnsvarlig expectedKakeAnsvarlig = _kakeAnsvarlige.First();
            Assert.That(_kakeFordeler.NextKakeAnsvarlig.Navn, Is.EqualTo(expectedKakeAnsvarlig.Navn));
        }

        [Test]
        public void ShouldFindNextKakeAnsvarlig()
        {
            KakeAnsvarlig previousKakeAnsvarlig = new KakeAnsvarlig(_kakeAnsvarlige.First().Navn, _kakeAnsvarlige.First().Epost);
            _kakeFordeler = new KakeFordeler(_kakeAnsvarlige, _ferieOversiktMock.Object, previousKakeAnsvarlig, DateTime.Now, DateTime.Now);
            
            _kakeFordeler.FindNextKakeAnsvarlig();

            KakeAnsvarlig expectedKakeAnsvarlig = _kakeAnsvarlige[1];
            Assert.That(_kakeFordeler.NextKakeAnsvarlig.Navn, Is.EqualTo(expectedKakeAnsvarlig.Navn));
        }
       
        [Test]
        public void ShouldFindNextKakeAnsvarlig_WhenPreviousWasLastInList()
        {
            KakeAnsvarlig previousKakeAnsvarlig = _kakeAnsvarlige.Last();
            _kakeFordeler = new KakeFordeler(_kakeAnsvarlige, _ferieOversiktMock.Object, previousKakeAnsvarlig, DateTime.Now, DateTime.Now);
            
            _kakeFordeler.FindNextKakeAnsvarlig();

            KakeAnsvarlig expectedKakeAnsvarlig = _kakeAnsvarlige.First();
            Assert.That(_kakeFordeler.NextKakeAnsvarlig.Navn, Is.EqualTo(expectedKakeAnsvarlig.Navn));
        }

        [Test]
        public void KakeFordelerShouldFindFirstFriday_WhenNoPreviousKakeDatoExistsAndNoHolidays()
        {
            DateTime today = new DateTime(2010, 04, 28);
            DateTime expectedKakeDato = new DateTime(2010, 04, 30);
            KakeFordeler kakeFordeler = new KakeFordeler(_kakeAnsvarlige, new FerieOversikt(new List<Ferie>()), null, DateTime.MinValue, today);

            Assert.That(kakeFordeler.NextKakeDato.ToShortDateString(), Is.EqualTo(expectedKakeDato.ToShortDateString()));
        }

        [Test]
        public void ShouldFindNextKakeDato_WhenNextFridayInTwoWeeksIsNotAHoliday()
        {
            DateTime fridayInTwoWeeks = new DateTime(2010, 05, 14);
            SetupFerieOversiktMock(fridayInTwoWeeks, false);
            DateTime today = new DateTime(2010, 04, 30);
            DateTime expectedKakeDato = new DateTime(2010, 05, 14);
            KakeFordeler kakeFordeler = new KakeFordeler(_kakeAnsvarlige, _ferieOversiktMock.Object, null, today, today);
            
            kakeFordeler.FindNextKakeDato(today);

            Assert.That(kakeFordeler.NextKakeDato, Is.EqualTo(expectedKakeDato));
        }

        [Test]
        public void ShouldFindNextKakeDato_WhenFerierIsNull()
        {
            DateTime today = new DateTime(2010, 04, 30);
            DateTime expectedKakeDato = new DateTime(2010, 05, 14);
            KakeFordeler kakeFordeler = new KakeFordeler(_kakeAnsvarlige, new FerieOversikt(new List<Ferie>()), null, today, today);
            
            kakeFordeler.FindNextKakeDato(today);

            Assert.That(kakeFordeler.NextKakeDato.ToShortDateString(), Is.EqualTo(expectedKakeDato.ToShortDateString()));
        }

        [Test]
        public void ShouldFindNextKakeDato_WhenFridayInTwoWeeksIsAHoliday()
        {
            DateTime nextFriday = new DateTime(2010, 05, 14);
            SetupFerieOversiktMock(nextFriday, true);
            DateTime today = new DateTime(2010, 04, 30);
            DateTime expectedKakeDato = new DateTime(2010, 05, 21);
            KakeFordeler kakeFordeler = new KakeFordeler(_kakeAnsvarlige, _ferieOversiktMock.Object, null, today, today);
            
            kakeFordeler.FindNextKakeDato(today);

            Assert.That(kakeFordeler.NextKakeDato, Is.EqualTo(expectedKakeDato));
        }

        private void SetupFerieOversiktMock(DateTime nextFriday, bool nextFridayIsAHoliday)
        {
            _ferieOversiktMock = new Mock<IFerieOversikt>();
            _ferieOversiktMock
                .Setup(m => m.IsHoliday(nextFriday))
                .Returns(nextFridayIsAHoliday);
        }

        private static IList<KakeAnsvarlig> CreateKakeAnsvarligeList()
        {
            List<KakeAnsvarlig> avdelinger = new List<KakeAnsvarlig>();
            avdelinger.Add(new KakeAnsvarlig("Christian", "ch@statkraft.com"));
            avdelinger.Add(new KakeAnsvarlig("Roy", "roy@statkraft.com"));
            return avdelinger;
        }
    }
}
