using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.Core.Services;
using Cake.UI.Controllers;
using Cake.UI.Models.Forms;
using NUnit.Framework;

namespace Cake.UnitTests.UI
{
    [TestFixture]
    public class HomeControllerTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _homeController = new HomeController(new DepartmentDaoStub(), new DepartmentServices(),
                                                 new CakeScheduleDaoStub(), new CakeScheduleServices());
        }

        #endregion

        private HomeController _homeController;

        private class DepartmentDaoStub : IDepartmentDao
        {
            #region IDepartmentDao Members

            public IList<Department> GetAll()
            {
                return DepartmentHelper.GetSampleList();
            }

            public void SaveAll(IList<Department> departments)
            {
                throw new NotImplementedException();
            }

            public Department Get(Guid id)
            {
                throw new NotImplementedException();
            }

            public void Add(Department department)
            {
                throw new NotImplementedException();
            }

            public void Delete(Department department)
            {
                throw new NotImplementedException();
            }

            public void Update(Department department)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        private class CakeScheduleDaoStub : ICakeScheduleDao
        {
            #region ICakeScheduleDao Members

            public CakeSchedule Get()
            {
                var cakeSchedule = new CakeSchedule
                                       {
                                           NextDate = new DateTime(2010, 3, 1)
                                       };

                cakeSchedule.AddHoliday(new DateTime(2010, 3, 20));
                cakeSchedule.AddHoliday(new DateTime(2010, 4, 10));

                return cakeSchedule;
            }

            public void Save(CakeSchedule cakeSchedule)
            {
            }

            #endregion
        }

        [Test]
        public void Ctor_DefaultSettings_CreatesObject()
        {
            Assert.That(_homeController, Is.Not.Null);
        }


        [Test]
        public void Index_StartPage_ReturnsStartPageForm()
        {
            var viewResult = _homeController.Index() as ViewResult;
            var startPageForm = viewResult.ViewData.Model as StartPageForm;

            Assert.That(startPageForm, Is.Not.Null);
        }
    }
}