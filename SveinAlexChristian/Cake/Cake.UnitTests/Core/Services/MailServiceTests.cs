
using System;
using System.Collections.Generic;
using Cake.Core.Configuration;
using Cake.Core.Domain;
using Cake.Core.Services;
using nDumbster.smtp;
using NUnit.Framework;

namespace Cake.UnitTests.Core.Services
{
    

    [TestFixture]
    public class MailServiceTests
    {
        private SimpleSmtpServer _server;
        private MailService _mailService;
        private MockAppConfigReader _appConfigReader;

        public MailServiceTests()
        {
            _server = null;
        }

        [SetUp]
        public void SetUp()
        {
            _server = SimpleSmtpServer.Start();
        }

        [TearDown]
        public void TearDown()
        {
            if (_server != null)
                _server.Stop();
           
        }

        private void SetupAppConfigReader()
        {
            _appConfigReader = new MockAppConfigReader();
            _appConfigReader.SetFromEmailAddress("svein.elgstoen@gmail.com");
            _appConfigReader.SetEmailSubject("subject");
            _appConfigReader.SetEmailBody("body {0}");
        }

        [Test]
        public void SendMail_SendOneMail_OneMailIsRecived()
        {
            SetupAppConfigReader();
            DateTime cakeDate = DateTime.Now.Date;
            _mailService = new MailService(new MockDepartmentServcies(), _appConfigReader, cakeDate);

            _mailService.SendMail("svein.elgstoen@gmail.com");
            Assert.AreEqual(1, _server.ReceivedEmail.Length, "server.ReceivedEmail.Length");
            Assert.AreEqual(1, _server.ReceivedEmailCount, "server.ReceivedEmailSize");

            SmtpMessage email = _server.ReceivedEmail[0];

            Assert.AreEqual("svein.elgstoen@gmail.com", email.Headers["To"]);
            Assert.AreEqual(_appConfigReader.FromMailAddress, email.Headers["From"]);
            Assert.AreEqual(_appConfigReader.MailSubject, email.Headers["Subject"]);
            Assert.AreEqual(string.Format(_appConfigReader.MailBody, cakeDate.Date), email.Body.Trim());
        }

        private class MockDepartmentServcies : IDepartmentServices
        {

            public IList<Department>  RotatetDepartmentList(IList<Department> departments)
            {
 	            throw new NotImplementedException();
            }

            public Department  GetFirstDepartmentInList(IList<Department> departments)
            {
                return new Department
                           {
                               ContactEmail = "svein.elgstoen@gmail.com",
                               ContactName = "Svein Elgstøen"
                           };
            }
        }

        private class MockAppConfigReader : IAppConfigReader
        {
            private int _timerRunInterval = 1;
            private int _hourOfDayToRunService = 1;
            private int _numberOfDaysBeforeCakeDayToSendMail;
            private string _mailSubject;
            private string _mailBody;
            private string _fromEmailAddress;

            public int TimerRunInterval
            {
                get
                {
                    return _timerRunInterval;
                }
            }
            public int HourOfDayToRunService
            {
                get
                {
                    return _hourOfDayToRunService;
                }
            }

            public int NumberOfDaysBeforeCakeDayToSendMail
            {
                get
                {
                    return _numberOfDaysBeforeCakeDayToSendMail;
                }
            }

            public string FromMailAddress
            {
                get
                {
                    return _fromEmailAddress;
                }
            }

            public string MailSubject
            {
                get
                {
                    return _mailSubject;
                }
            }

            public string MailBody
            {
                get
                {
                    return _mailBody;
                }
            }

            public string MailHost
            {
                get
                {
                    return "localhost";
                }
            }

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

            public void SetEmailSubject(string mailSubject)
            {
                _mailSubject = mailSubject;
            }

            public void SetEmailBody(string mailBody)
            {
                _mailBody = mailBody;
            }

            public void SetFromEmailAddress(string fromEmailAddress)
            {
                _fromEmailAddress = fromEmailAddress;
            }





            #region IAppConfigReader Members


            public string RepositoryPath
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region IAppConfigReader Members


            public string LogPath
            {
                get { throw new NotImplementedException(); }
            }

            #endregion
        }
    }
}
