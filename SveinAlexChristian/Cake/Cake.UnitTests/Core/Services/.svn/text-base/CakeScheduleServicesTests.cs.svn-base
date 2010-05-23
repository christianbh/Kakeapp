using System;
using Cake.Core.Domain;
using Cake.Core.Services;
using NUnit.Framework;

namespace Cake.UnitTests.Core.Services
{
    [TestFixture]
    public class CakeScheduleServicesTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _cakeScheduleServices = new CakeScheduleServices();
        }

        #endregion

        private CakeScheduleServices _cakeScheduleServices;

        private DateTime GetSecondFridayFromToday()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                return DateTime.Now.Date.AddDays(14);
            }

            DateTime nextFriday = DateTime.Now.Date;

            while (nextFriday.DayOfWeek != DayOfWeek.Friday)
            {
                nextFriday = nextFriday.AddDays(1);
            }

            return nextFriday.AddDays(7);
        }


        [Test]
        public void SetNextCakeDate_CakeScheduleIsNull_ShouldSetSecondFridayFromToday()
        {
            var currentDate = new DateTime(2010, 5, 10);
            CakeSchedule cakeSchedule = _cakeScheduleServices.SetNextCakeDate(null, currentDate);
            var secondFridayFromToday = new DateTime(2010, 5, 21);

            Assert.That(cakeSchedule.NextDate, Is.EqualTo(secondFridayFromToday));
            Assert.That(cakeSchedule.NextDate.DayOfWeek, Is.EqualTo(DayOfWeek.Friday));
        }

        [Test]
        public void SetNextCakeDate_ExpectedCakeDateIsHolliday_ShouldSetSecondThirdFromToday()
        {
            var cakeSchedule = new CakeSchedule();
            var currentDate = new DateTime(2010, 5, 10);
            var secondFridayFromToday = new DateTime(2010, 5, 21);
            var thirdFridayFromToday = new DateTime(2010, 5, 28);
            cakeSchedule.AddHoliday(secondFridayFromToday);
            cakeSchedule = _cakeScheduleServices.SetNextCakeDate(cakeSchedule, currentDate);

            Assert.That(cakeSchedule.NextDate, Is.EqualTo(thirdFridayFromToday));
            Assert.That(cakeSchedule.NextDate.DayOfWeek, Is.EqualTo(DayOfWeek.Friday));
        }
    }
}