using System;
using System.Collections.Generic;
using Cake.Core.Configuration;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.Core.Services;
using Cake.Scheduler;
using NUnit.Framework;

namespace Cake.IntegrationTests.Core.Services
{
    [TestFixture]
    public class ScheduleWorkerTests
    {
        #region Setup/Teardown

        [TearDown]
        public void TearDown()
        {
            if (_scheduleWorker != null)
                _scheduleWorker.StopScedule();
        }

        #endregion

        private readonly MockAppConfigReader _appConfigReader = new MockAppConfigReader();
        private readonly MockCakeScheduleServices _cakeScheduleServices = new MockCakeScheduleServices();
        private readonly MockMailService _mailService = new MockMailService();
        private readonly MockCakeScheduleDao _cakeSheduleDao = new MockCakeScheduleDao();
        private readonly MockDepartmentDao _departmentDao = new MockDepartmentDao();
        private readonly IDepartmentServices _departmentServices = new DepartmentServices();
        private ScheduleWorker _scheduleWorker;
        private bool _finishedEventFired;
        private bool _cakeScheduleIsRun;
        private bool _mailServiceIsRun;


        [Test]
        public void CakeScheduleFinished_CakeDateIsInTwoDays_MailServiseIsNotRun()
        {
            
            SetUpScheduleWorker(2);

            DateTime endTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < endTime)
            {
            }

            Assert.That(_mailServiceIsRun, Is.EqualTo(false));
        }

        [Test]
        public void CakeScheduleFinished_CakeDateIsTomorrow_MailServiseIsRun()
        {
            
            SetUpScheduleWorker(1);

            DateTime endTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < endTime)
            {
            }

            Assert.That(_mailServiceIsRun, Is.EqualTo(true));
        }

        [Test]
        public void CakeScheduleFinished_CakeDateWasTwoDaysAgo_CakeRescheduleIsNotRun()
        {
            SetUpScheduleWorker(-2);

            DateTime endTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < endTime)
            {
            }

            Assert.That(_cakeScheduleIsRun, Is.EqualTo(false));
        }

        [Test]
        public void CakeScheduleFinished_CakeDateWasYesterday_CakeRescheduleIsRun()
        {
            
            SetUpScheduleWorker(-1);

            DateTime endTime = DateTime.Now.AddSeconds(2);
            while (DateTime.Now < endTime)
            {
            }

            Assert.That(_cakeScheduleIsRun, Is.EqualTo(true));
        }

        [Test]
        public void CakeScheduleFinished_ScheduleIsStarted_EventIsFired()
        {
            _finishedEventFired = false;
            _appConfigReader.SetTimerRunInterval(1);
            SetUpScheduleWorker(1);

            DateTime endTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < endTime)
            {
            }

            Assert.That(_finishedEventFired, Is.EqualTo(true));
        }

        public void SetUpScheduleWorker(int daysToAdd)
        {
            _mailServiceIsRun = false;
            _appConfigReader.SetTimerRunInterval(1);
            _appConfigReader.SetHourOfDayToRunService(DateTime.Now.Hour);
            _cakeSheduleDao.SetNextCakeDateMock(DateTime.Now.AddDays(daysToAdd));
            _scheduleWorker = new ScheduleWorker(_cakeScheduleServices, _departmentServices, _mailService,
                                                 _cakeSheduleDao, _departmentDao, _appConfigReader);
            _scheduleWorker.CakeScheduleFinished += ScheduleFinished;
            _scheduleWorker.StartSchedule();
        }


        private void ScheduleFinished(object sender, CakeScheduleEventArgs eventArgs)
        {
            _finishedEventFired = true;
            _cakeScheduleIsRun = eventArgs.CakeRescheduleServiceIsRun;
            _mailServiceIsRun = eventArgs.MailServiceIsRun;
        }

        private class MockAppConfigReader : IAppConfigReader
        {
            private int _hourOfDayToRunService = 1;
            private int _numberOfDaysBeforeCakeDayToSendMail;
            private int _timerRunInterval = 1;

            #region IAppConfigReader Members

            public int TimerRunInterval
            {
                get { return _timerRunInterval; }
            }

            public int HourOfDayToRunService
            {
                get { return _hourOfDayToRunService; }
            }

            public int NumberOfDaysBeforeCakeDayToSendMail
            {
                get { return _numberOfDaysBeforeCakeDayToSendMail; }
            }

            public string FromMailAddress
            {
                get { throw new NotImplementedException(); }
            }

            public string MailSubject
            {
                get { throw new NotImplementedException(); }
            }

            public string MailBody
            {
                get { throw new NotImplementedException(); }
            }

            public string MailHost
            {
                get { throw new NotImplementedException(); }
            }

            public string RepositoryPath
            {
                get { throw new NotImplementedException(); }
            }

            public string LogPath
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            public void SetTimerRunInterval(int timerRunInterval)
            {
                _timerRunInterval = timerRunInterval;
            }

            public void SetHourOfDayToRunService(int hourOfDayToRunService)
            {
                _hourOfDayToRunService = hourOfDayToRunService;
            }

            public void SetNumberOfDaysBeforeCakeDayToSendMail(int numberOfDays)
            {
                _numberOfDaysBeforeCakeDayToSendMail = numberOfDays;
            }
        }

        private class MockCakeScheduleServices : ICakeScheduleServices
        {
            private readonly DateTime _nextCakeDate = DateTime.Now.AddDays(-1);

            #region ICakeScheduleServices Members

            public CakeSchedule SetNextCakeDate(CakeSchedule cakeSchedule, DateTime currentDate)
            {
                return new CakeSchedule
                           {
                               NextDate = _nextCakeDate
                           };
            }

            #endregion
        }

        private class MockCakeScheduleDao : ICakeScheduleDao
        {
            private DateTime _nextCakeDate = DateTime.Now.AddDays(-1);

            #region ICakeScheduleDao Members

            public CakeSchedule Get()
            {
                return new CakeSchedule
                           {
                               NextDate = _nextCakeDate
                           };
            }

            public void Save(CakeSchedule cakeSchedule)
            {
            }

            #endregion

            public void SetNextCakeDateMock(DateTime nextCakeDate)
            {
                _nextCakeDate = nextCakeDate;
            }
        }

        private class MockDepartmentDao : IDepartmentDao
        {
            #region IDepartmentDao Members

            IList<Department> IDepartmentDao.GetAll()
            {
                return new List<Department>();
            }

            void IDepartmentDao.SaveAll(IList<Department> departments)
            {
            }

            Department IDepartmentDao.Get(Guid id)
            {
                throw new NotImplementedException();
            }

            void IDepartmentDao.Add(Department department)
            {
                throw new NotImplementedException();
            }

            void IDepartmentDao.Delete(Department department)
            {
                throw new NotImplementedException();
            }

            void IDepartmentDao.Update(Department department)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        private class MockMailService : IMailService
        {
            #region IMailService Members

            public void SendMailToNextCakeDepartment(IList<Department> departments)
            {
                throw new NotImplementedException();
            }

            public void SendMail(string emailAdress)
            {
            }

            #endregion
        }

        
    }
}