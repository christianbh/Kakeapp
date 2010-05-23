using System;
using System.IO;
using Cake.Core.Domain;
using Cake.DAL;
using NUnit.Framework;

namespace Cake.IntegrationTests.DAL
{
    [TestFixture]
    public class CakeScheduleDaoTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _cakeScheduleDao = new CakeScheduleDao(Path.GetTempPath());
            DeleteCakeScheduleFile();
        }

        #endregion

        private CakeScheduleDao _cakeScheduleDao;

        private static CakeSchedule GetSampleCakeScheduleWithHolidays()
        {
            var cakeSchedule = new CakeSchedule
                                   {
                                       NextDate = new DateTime(2010, 3, 1)
                                   };

            cakeSchedule.AddHoliday(new DateTime(2010, 3, 20));
            cakeSchedule.AddHoliday(new DateTime(2010, 4, 10));

            return cakeSchedule;
        }

        private void DeleteCakeScheduleFile()
        {
            if (File.Exists(_cakeScheduleDao.CakeScheduleFileName))
            {
                File.Delete(_cakeScheduleDao.CakeScheduleFileName);
            }
        }

        [Test]
        public void Get_ReadNoneExistingFile_ShouldReturnUnAssignedCakeSchedule()
        {
            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();

            Assert.That(cakeSchedule.NextDate, Is.EqualTo(new DateTime()));
        }

        [Test]
        public void Save_CakeScheduleIsNull_SavesCakeSchedule()
        {
            _cakeScheduleDao.Save(null);

            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();

            Assert.That(cakeSchedule.Holidays.Count, Is.EqualTo(0));
        }

        [Test]
        public void Save_CakeSchedule_SavesCakeSchedule()
        {
            _cakeScheduleDao.Save(GetSampleCakeScheduleWithHolidays());

            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();

            Assert.That(cakeSchedule.Holidays.Count, Is.EqualTo(2));
        }
    }
}