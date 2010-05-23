using System;
using System.Collections.Generic;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.Core.Services;

namespace Cake.UI.Models.Forms
{
    public class StartPageForm
    {
        private readonly ICakeScheduleDao _cakeScheduleDao;
        private readonly ICakeScheduleServices _cakeScheduleServices;
        private readonly IDepartmentDao _departmentDao;
        private readonly IDepartmentServices _departmentServices;

        public StartPageForm(IDepartmentDao departmentDao, IDepartmentServices departmentServices,
                             ICakeScheduleDao cakeScheduleDao, ICakeScheduleServices cakeScheduleServices)
        {
            _departmentDao = departmentDao;
            _departmentServices = departmentServices;
            _cakeScheduleDao = cakeScheduleDao;
            _cakeScheduleServices = cakeScheduleServices;

            BuildStartPage();
        }


        public Department NextDepartment { get; private set; }
        public CakeSchedule CakeSchedule { get; private set; }


        private void BuildStartPage()
        {
            IList<Department> departments = _departmentDao.GetAll();
            NextDepartment = _departmentServices.GetFirstDepartmentInList(departments);
            CakeSchedule = _cakeScheduleServices.SetNextCakeDate(_cakeScheduleDao.Get(), DateTime.Now.Date);
        }
    }
}