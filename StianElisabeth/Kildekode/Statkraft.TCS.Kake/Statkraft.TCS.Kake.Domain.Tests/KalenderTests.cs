using System;
using NUnit.Framework;

namespace Statkraft.TCS.Kake.Domain.Tests
{
    public class KalenderTests
    {
        private Kalender _kalender;

        [SetUp]
        public void SetUp()
        {
            _kalender = new Kalender();
        }

        [Test]
        public void ShouldFindNextFriday_WhenTodayIsBeforeFriday()
        {
            DateTime wednesday = new DateTime(2010, 04, 28);

            DateTime actualNextFriday = _kalender.FindNextFriday(wednesday);

            DateTime expectedNextFriday = new DateTime(2010, 04, 30);
            Assert.That(actualNextFriday.ToShortDateString(), Is.EqualTo(expectedNextFriday.ToShortDateString()));
        }

        [Test]
        public void ShouldFindNextFriday_WhenTodayIsFriday()
        {
            DateTime friday = new DateTime(2010, 04, 30);

            DateTime actualNextFriday = _kalender.FindNextFriday(friday);

            DateTime expectedNextFriday = new DateTime(2010, 05, 07);
            Assert.That(actualNextFriday.ToShortDateString(), Is.EqualTo(expectedNextFriday.ToShortDateString()));
        }

        [Test]
        public void ShouldFindNextFriday_WhenTodayIsSunday()
        {
            DateTime sunday = new DateTime(2010, 05, 02);

            DateTime actualNextFriday = _kalender.FindNextFriday(sunday);

            DateTime expectedNextFriday = new DateTime(2010, 05, 07);
            Assert.That(actualNextFriday.ToShortDateString(), Is.EqualTo(expectedNextFriday.ToShortDateString()));
        }

        [Test]
        public void ShouldFindNextFriday_WhenTodayIsSaturday()
        {
            DateTime saturday = new DateTime(2010, 05, 01);

            DateTime actualNextFriday = _kalender.FindNextFriday(saturday);

            DateTime expectedNextFriday = new DateTime(2010, 05, 07);
            Assert.That(actualNextFriday.ToShortDateString(), Is.EqualTo(expectedNextFriday.ToShortDateString()));
        }
    }
}
