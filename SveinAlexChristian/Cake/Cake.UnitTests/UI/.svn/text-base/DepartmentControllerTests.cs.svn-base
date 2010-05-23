using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.UI.Controllers;
using NUnit.Framework;

namespace Cake.UnitTests.UI
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _departmentDaoMock = new DepartmentDaoStub();
            _departmentController = new DepartmentController(_departmentDaoMock);
        }

        #endregion

        private DepartmentController _departmentController;
        private IDepartmentDao _departmentDaoMock;

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
                return new Department
                           {
                               Id = id,
                               ContactEmail = "test@test.no",
                               ContactName = "Test Person",
                               SortOrder = 1
                           };
            }

            public void Add(Department department)
            {
                //throw new NotImplementedException();
            }

            public void Delete(Department department)
            {
                //throw new NotImplementedException();
            }

            public void Update(Department department)
            {
                //throw new NotImplementedException();
            }

            #endregion
        }


        [Test]
        public void Create_Get_ReturnsCreateView()
        {
            var viewResult = _departmentController.Create() as ViewResult;
            ViewDataDictionary viewData = viewResult.ViewData;
            object viewDepartment = viewData.Model;

            Assert.That(viewDepartment, Is.Null);
        }

        [Test]
        public void Create_PostNull_ReturnsView()
        {
            Department department = null;
            var result = _departmentController.Create(department) as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Create_PostValidData_RedirectsToList()
        {
            var department = new Department
                                 {
                                     ContactEmail = "new@contact.no",
                                     ContactName = "New Contact"
                                 };

            var result = _departmentController.Create(department) as RedirectToRouteResult;
            Assert.That(result.RouteValues["action"].ToString(), Is.EqualTo("List"));
        }

        [Test]
        public void Ctor_DefaultSettings_CreatesObject()
        {
            Assert.That(_departmentController, Is.Not.Null);
        }


        [Test]
        public void Delete_GetWithExistingDepartment_ReturnsConfirmationView()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E3");
            Department department = _departmentDaoMock.Get(id);

            var viewResult = _departmentController.Delete(department.Id) as ViewResult;
            var viewDepartment = (Department) viewResult.ViewData.Model;
            Assert.That(viewDepartment.Id, Is.EqualTo(id));
        }

        [Test]
        public void Delete_PostNull_ReturnsView()
        {
            Department department = null;
            var result = _departmentController.Delete(department) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.ContainsValue("List"), Is.True);
        }

        [Test]
        public void Delete_PostWithExistingDepartment_RedirectsToList()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E3");
            Department department = _departmentDaoMock.Get(id);

            var result = _departmentController.Delete(department) as RedirectToRouteResult;
            Assert.That(result.RouteValues.ContainsValue("List"), Is.True);
        }


        [Test]
        public void Edit_GetWithExistingDepartment_FillsModel()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E3");
            Department department = _departmentDaoMock.Get(id);

            var viewResult = _departmentController.Edit(department.Id) as ViewResult;
            var viewDepartment = (Department) viewResult.ViewData.Model;
            Assert.That(viewDepartment.Id, Is.EqualTo(id));
        }

        [Test]
        public void Edit_PostWithExistingDepartment_RedirectsToList()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E3");
            Department department = _departmentDaoMock.Get(id);

            var result = _departmentController.Edit(department) as RedirectToRouteResult;
            Assert.That(result.RouteValues["action"].ToString(), Is.EqualTo("List"));
        }

        [Test]
        public void List_StartPage_ReturnsListOfDepartments()
        {
            var viewResult = _departmentController.List() as ViewResult;
            ViewDataDictionary viewData = viewResult.ViewData;
            var viewDepartments = viewData.Model as List<Department>;

            Assert.That(viewDepartments.Count, Is.EqualTo(3));
        }
    }
}