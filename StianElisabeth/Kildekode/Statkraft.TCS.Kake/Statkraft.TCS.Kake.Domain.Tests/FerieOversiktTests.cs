using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Statkraft.TCS.Kake.Domain.Tests
{
    [TestFixture]
    public class FerieOversiktTests
    {
        IList<Ferie> _ferier;
        private FerieOversikt _ferieOversikt;

        [SetUp]
        public void SetUp()
        {
            _ferier = CreateFerieList();
            _ferieOversikt = new FerieOversikt(_ferier);
        }

        [Test]
        public void ShouldReturnTrue_WhenGivenDayIsHoliday()
        {
            DateTime dayToCheck = new DateTime(2010, 04, 30);

            bool actualHolidayStatus = _ferieOversikt.IsHoliday(dayToCheck);
            bool expectedHolidayStatus = true;

            Assert.That(actualHolidayStatus, Is.EqualTo(expectedHolidayStatus));
        }

        [Test]
        public void ShouldReturnFalse_WhenGivenDayIsNotAHoliday()
        {
            DateTime dayToCheck = new DateTime(2010, 05, 01);

            bool actualHolidayStatus = _ferieOversikt.IsHoliday(dayToCheck);
            bool expectedHolidayStatus = false;

            Assert.That(actualHolidayStatus, Is.EqualTo(expectedHolidayStatus));
        }

        private static IList<Ferie> CreateFerieList()
        {
            IList<Ferie> ferier = new List<Ferie>();
            Ferie holiday1 = new Ferie(new DateTime(2010, 04, 30), new DateTime(2010, 04, 30));
            Ferie holiday2 = new Ferie (new DateTime(2010, 05, 05), new DateTime(2010, 05, 20));
            ferier.Add(holiday1);
            ferier.Add(holiday2);
            return ferier;
        }
    }
}
