using System;
using Cake.Core.Domain;
using NUnit.Framework;

namespace Cake.UnitTests.Core.Domain
{
    [TestFixture]
    public class CakeScheduleTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _cakeSchedule = new CakeSchedule();
        }

        #endregion

        private CakeSchedule _cakeSchedule;


        [Test]
        public void AddHoliday_DateIsInHolidayList_ReturnsFalse()
        {
            var newHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(newHolidayDate);

            bool result = _cakeSchedule.AddHoliday(newHolidayDate);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AddHoliday_DateIsNotInHolidayList_ReturnsTrue()
        {
            var newHolidayDate = new DateTime(2010, 5, 10);

            bool result = _cakeSchedule.AddHoliday(newHolidayDate);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Ctor_DefaultSettings_CreatesCakeSchedule()
        {
            Assert.That(_cakeSchedule, Is.Not.Null);
        }


        [Test]
        public void DeleteHoliday_DateIsInHolidayList_ReturnsTrue()
        {
            var newHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(newHolidayDate);

            bool result = _cakeSchedule.DeleteHoliday(newHolidayDate);

            Assert.That(result, Is.True);
        }

        [Test]
        public void DeleteHoliday_DateIsNotInHolidayList_ReturnsFalse()
        {
            var newHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(newHolidayDate);

            bool result = _cakeSchedule.DeleteHoliday(newHolidayDate.AddDays(1));

            Assert.That(result, Is.False);
        }


        [Test]
        public void EditHoliday_ExistingDateDoesntExist_ReturnsFalse()
        {
            var existingHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(existingHolidayDate);

            bool result = _cakeSchedule.EditHoliday(existingHolidayDate.AddDays(1), existingHolidayDate.AddDays(2));

            Assert.That(result, Is.False);
        }

        [Test]
        public void EditHoliday_NewDateExists_ReturnsFalse()
        {
            var newDateThatExists = new DateTime(2010, 5, 17);
            var existingHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(newDateThatExists);
            _cakeSchedule.AddHoliday(existingHolidayDate);

            bool result = _cakeSchedule.EditHoliday(existingHolidayDate, newDateThatExists);

            Assert.That(result, Is.False);
        }

        [Test]
        public void EditHoliday_ValidSettings_RemovesExistingDateAndAddsNew()
        {
            var newHolidayDate = new DateTime(2010, 5, 17);
            var existingHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(existingHolidayDate);

            bool result = _cakeSchedule.EditHoliday(existingHolidayDate, newHolidayDate);

            Assert.That(result, Is.True);
            Assert.That(_cakeSchedule.IsValidHolidayDate(existingHolidayDate), Is.False);
            Assert.That(_cakeSchedule.IsValidHolidayDate(newHolidayDate), Is.True);
        }

        [Test]
        public void IsValidHolidayDate_DateInHolidayList_ReturnsTrue()
        {
            var newHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(newHolidayDate);

            Assert.That(_cakeSchedule.IsValidHolidayDate(newHolidayDate), Is.True);
        }

        [Test]
        public void IsValidHolidayDate_DateNotInHolidayList_ReturnFalse()
        {
            var newHolidayDate = new DateTime(2010, 5, 10);
            _cakeSchedule.AddHoliday(newHolidayDate.AddDays(1));

            Assert.That(_cakeSchedule.IsValidHolidayDate(newHolidayDate), Is.False);
        }
    }
}