using System;
using System.ServiceProcess;
using Cake.Core.Configuration;
using Cake.Core.DataInterfaces;
using Cake.Core.Logging;
using Cake.Core.Services;
using Cake.DAL;
using Cake.Scheduler;

namespace Scheduler
{
    public partial class CakeScheduleService : ServiceBase
    {
        private readonly IAppConfigReader _appConfigReader = new AppConfigReader();
        private readonly ICakeScheduleServices _cakeScheduleServices = new CakeScheduleServices();
        private readonly ICakeScheduleDao _cakeSheduleDao;
        private readonly IDepartmentDao _departmentDao;
        private readonly IDepartmentServices _departmentServices = new DepartmentServices();
        private readonly IMailService _mailService;
        private readonly ScheduleWorker _scheduleWorker;
        private readonly ServiceLogger _serviceLogger;

        public CakeScheduleService()
        {
            InitializeComponent();
            _mailService = new MailService(_departmentServices, _appConfigReader, DateTime.Now.Date);
            _cakeSheduleDao = new CakeScheduleDao(_appConfigReader.RepositoryPath);
            _departmentDao = new DepartmentDao(_appConfigReader.RepositoryPath);
            _scheduleWorker = new ScheduleWorker(_cakeScheduleServices, _departmentServices, _mailService,
                                                 _cakeSheduleDao, _departmentDao, _appConfigReader);
            _serviceLogger = new ServiceLogger(_appConfigReader.LogPath);
            _scheduleWorker.CakeScheduleFinished += ScheduleFinished;
        }

        protected override void OnStart(string[] args)
        {
            _scheduleWorker.StartSchedule();
            _serviceLogger.Write("Service started: " + DateTime.Now);
        }

        protected override void OnStop()
        {
            _scheduleWorker.StopScedule();
            _serviceLogger.Write("Service stopped: " + DateTime.Now);
        }

        private void ScheduleFinished(object sender, CakeScheduleEventArgs eventArgs)
        {
            _serviceLogger.Write("Service run: " + DateTime.Now);

            if (eventArgs.CakeRescheduleServiceIsRun)
            {
                _serviceLogger.Write("Cake Schedule Service run: " + DateTime.Now);
            }

            if (eventArgs.MailServiceIsRun)
            {
                _serviceLogger.Write("Mail Service run: " + DateTime.Now);
            }
        }
    }
}