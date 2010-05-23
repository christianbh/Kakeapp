using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.UI.Controllers;
using Cake.UI.Models.Forms;
using NUnit.Framework;

namespace Cake.UnitTests.UI
{
    [TestFixture]
    public class CakeScheduleControllerTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _cakeScheduleDao = new CakeScheduleDaoStub();
            _cakeScheduleController = new CakeScheduleController(_cakeScheduleDao);
        }

        #endregion

        private CakeScheduleController _cakeScheduleController;
        private CakeScheduleDaoStub _cakeScheduleDao;

        private FormCollection GetFormCollectionWithValidDates()
        {
            return new FormCollection
                       {
                           {"newDate", _cakeScheduleDao.GetNoneExistingHoliday().ToShortDateString()},
                           {"existingDate", _cakeScheduleDao.GetExistingsHoliday().ToShortDateString()}
                       };
        }

        private FormCollection GetFormCollectionWithMissingExistingHoliday()
        {
            return new FormCollection
                       {
                           {"newDate", _cakeScheduleDao.GetNoneExistingHoliday().ToShortDateString()}
                       };
        }

        private FormCollection GetFormCollectionWithMissingNewHoliday()
        {
            return new FormCollection
                       {
                           {"existingDate", _cakeScheduleDao.GetExistingsHoliday().ToShortDateString()}
                       };
        }

        private FormCollection GetFormCollectionWithInvalidExistingDate()
        {
            return new FormCollection
                       {
                           {"newDate", _cakeScheduleDao.GetNoneExistingHoliday().ToShortDateString()},
                           {"existingDate", _cakeScheduleDao.GetNoneExistingHoliday().ToShortDateString()}
                       };
        }

        private FormCollection GetFormCollectionWithNewDate()
        {
            return new FormCollection
                       {
                           {"newDate", _cakeScheduleDao.GetNoneExistingHoliday().ToShortDateString()}
                       };
        }

        private FormCollection GetFormCollectionWithInvalidNewDate()
        {
            return new FormCollection
                       {
                           {"newDate", _cakeScheduleDao.GetExistingsHoliday().ToShortDateString()}
                       };
        }

        private class CakeScheduleDaoStub : ICakeScheduleDao
        {
            #region ICakeScheduleDao Members

            public CakeSchedule Get()
            {
                var cakeSchedule = new CakeSchedule();
                foreach (DateTime holiday in GetHolidays())
                {
                    cakeSchedule.AddHoliday(holiday);
                }

                return cakeSchedule;
            }

            public void Save(CakeSchedule cakeSchedule)
            {
                // do nothing here
            }

            #endregion

            public DateTime GetNoneExistingHoliday()
            {
                return new DateTime(2010, 8, 1);
            }

            public DateTime GetExistingsHoliday()
            {
                return new DateTime(2010, 7, 20);
            }

            public List<DateTime> GetHolidays()
            {
                return new List<DateTime>
                           {
                               new DateTime(2010, 1, 20),
                               new DateTime(2010, 5, 17),
                               new DateTime(2010, 7, 2),
                               GetExistingsHoliday()
                           };
            }
        }


        [Test]
        public void Create_CreatesViewResult()
        {
            ActionResult viewResult = _cakeScheduleController.Create();

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Create_PostWithExistingHoliday_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithInvalidNewDate();
            var viewResult = _cakeScheduleController.Create(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Create_PostWithMissingNewDate_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithMissingNewHoliday();
            var viewResult = _cakeScheduleController.Create(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Create_PostWithNoneExistingHoliday_RedirectsToIndex()
        {
            FormCollection formCollection = GetFormCollectionWithNewDate();
            var viewResult = _cakeScheduleController.Create(formCollection) as RedirectToRouteResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.RouteValues.ContainsValue("Index"), Is.True);
        }


        [Test]
        public void Delete_GetWithExistingHoliday_FillsModel()
        {
            DateTime dateToEdit = _cakeScheduleDao.GetExistingsHoliday();
            var viewResult = _cakeScheduleController.Delete(dateToEdit.ToShortDateString()) as ViewResult;

            var model = viewResult.ViewData.Model as HolidayEditForm;

            Assert.That(model.ExistingDate, Is.EqualTo(dateToEdit));
        }

        [Test]
        public void Delete_GetWithNoneExistingHoliday_RedirectsToIndex()
        {
            DateTime dateToDelete = _cakeScheduleDao.GetNoneExistingHoliday();
            var viewResult = _cakeScheduleController.Delete(dateToDelete.ToShortDateString()) as RedirectToRouteResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void Delete_GetWithNull_RedirectsToIndex()
        {
            string input = null;
            var viewResult = _cakeScheduleController.Delete(input) as RedirectToRouteResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void Delete_PostWithExistingHoliday_RedirectsToIndex()
        {
            FormCollection formCollection = GetFormCollectionWithValidDates();
            var viewResult = _cakeScheduleController.Delete(formCollection) as RedirectToRouteResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void Delete_PostWithMissingExistingDate_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithMissingExistingHoliday();
            var viewResult = _cakeScheduleController.Delete(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Delete_PostWithNoneExistingHoliday_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithInvalidExistingDate();
            var viewResult = _cakeScheduleController.Delete(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Edit_GetWithExistingHoliday_FillsModel()
        {
            DateTime dateToEdit = _cakeScheduleDao.GetExistingsHoliday();
            var viewResult = _cakeScheduleController.Edit(dateToEdit.ToShortDateString()) as ViewResult;

            var model = viewResult.ViewData.Model as HolidayEditForm;

            Assert.That(model.ExistingDate, Is.EqualTo(dateToEdit));
        }

        [Test]
        public void Edit_GetWithNoneExistingHoliday_RedirectsToIndex()
        {
            DateTime dateToEdit = _cakeScheduleDao.GetNoneExistingHoliday();
            var viewResult = _cakeScheduleController.Edit(dateToEdit.ToShortDateString()) as RedirectToRouteResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void Edit_PostWithExistingHoliday_RedirectsToIndex()
        {
            FormCollection formCollection = GetFormCollectionWithValidDates();
            var viewResult = _cakeScheduleController.Edit(formCollection) as RedirectToRouteResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void Edit_PostWithInvalidExistingDate_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithInvalidExistingDate();
            var viewResult = _cakeScheduleController.Edit(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Edit_PostWithMissingExistingDate_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithMissingExistingHoliday();
            var viewResult = _cakeScheduleController.Edit(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Edit_PostWithMissingNewDate_ReturnsView()
        {
            FormCollection formCollection = GetFormCollectionWithMissingNewHoliday();
            var viewResult = _cakeScheduleController.Edit(formCollection) as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public void Index_ShouldReturnListOfHolidays()
        {
            var viewResult = _cakeScheduleController.Index() as ViewResult;
            var cakeSchedule = viewResult.ViewData.Model as CakeSchedule;
            List<DateTime> expectedHolidays = _cakeScheduleDao.GetHolidays();

            Assert.That(cakeSchedule.Holidays.Count, Is.EqualTo(expectedHolidays.Count));
        }
    }
}