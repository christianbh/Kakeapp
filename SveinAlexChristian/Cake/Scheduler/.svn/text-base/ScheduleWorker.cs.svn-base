using System;
using System.Collections.Generic;
using System.Timers;
using Cake.Core.Configuration;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.Core.Services;

namespace Cake.Scheduler
{
    public class ScheduleWorker
    {
        #region Delegates

        public delegate void CakeScheduleFinishedDelegate(object sender, CakeScheduleEventArgs e);

        #endregion

        private readonly ICakeScheduleDao _cakeScheduleDao;

        private readonly ICakeScheduleServices _cakeScheduleServices;
        private readonly IDepartmentDao _departmentDao;
        private readonly IDepartmentServices _departmentServices;
        private readonly IMailService _mailService;
        private readonly Timer _timer;
        public IAppConfigReader _appConfigReader;

        public ScheduleWorker(ICakeScheduleServices cakeScheduleServices, IDepartmentServices departmentServices,
                              IMailService mailService,
                              ICakeScheduleDao cakeScheduleDao, IDepartmentDao departmentDao,
                              IAppConfigReader appConfigReader)
        {
            _cakeScheduleServices = cakeScheduleServices;
            _departmentServices = departmentServices;
            _mailService = mailService;
            _cakeScheduleDao = cakeScheduleDao;
            _departmentDao = departmentDao;
            _appConfigReader = appConfigReader;

            _timer = new Timer(_appConfigReader.TimerRunInterval);
            _timer.Elapsed += Timer_Elapsed;
        }

        public event CakeScheduleFinishedDelegate CakeScheduleFinished;

        public void StartSchedule()
        {
            if (_timer != null)
                _timer.Start();
        }

        public void StopScedule()
        {
            if (_timer != null)
                _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs args)
        {
            bool cakeRescheduleIsRun = RunCakeReschedule();
            bool mailServiceIsRun = RunMailService();

            if (CakeScheduleFinished != null)
            {
                var cakeScheduleEventArgs = new CakeScheduleEventArgs
                                                {
                                                    CakeRescheduleServiceIsRun = cakeRescheduleIsRun,
                                                    MailServiceIsRun = mailServiceIsRun
                                                };

                CakeScheduleFinished(this, cakeScheduleEventArgs);
            }
        }

        private bool RunCakeReschedule()
        {
            bool cakeScheduleIsRun = false;

            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();
            if (cakeSchedule.NextDate.Date == DateTime.Now.Date.AddDays(-1))
            {
                if (DateTime.Now.Hour == _appConfigReader.HourOfDayToRunService)
                {
                    _cakeScheduleServices.SetNextCakeDate(cakeSchedule, DateTime.Now.Date);
                    _cakeScheduleDao.Save(cakeSchedule);

                    IList<Department> departments = _departmentDao.GetAll();
                    departments = _departmentServices.RotatetDepartmentList(departments);
                    _departmentDao.SaveAll(departments);

                    cakeScheduleIsRun = true;
                }
            }

            return cakeScheduleIsRun;
        }

        private bool RunMailService()
        {
            bool mailServiceIsRun = false;

            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();
            if (cakeSchedule.NextDate.Date == DateTime.Now.Date.AddDays(1))
            {
                if (DateTime.Now.Hour == _appConfigReader.HourOfDayToRunService)
                {
                    IList<Department> departments = _departmentDao.GetAll();
                    Department department = _departmentServices.GetFirstDepartmentInList(departments);
                    _mailService.SendMail(department.ContactEmail);
                    mailServiceIsRun = true;
                }
            }

            return mailServiceIsRun;
        }
    }

    public class CakeScheduleEventArgs : EventArgs
    {
        public bool CakeRescheduleServiceIsRun { get; set; }
        public bool MailServiceIsRun { get; set; }
    }
}