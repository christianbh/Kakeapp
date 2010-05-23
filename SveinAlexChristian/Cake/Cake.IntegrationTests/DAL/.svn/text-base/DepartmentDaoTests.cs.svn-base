using System;
using System.Collections.Generic;
using System.IO;
using Cake.Core.Domain;
using Cake.DAL;
using NUnit.Framework;

namespace Cake.IntegrationTests.DAL
{
    [TestFixture]
    public class DepartmentDaoTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _departmentDao = new DepartmentDao(Path.GetTempPath());
            _departmentDao.SaveAll(GetSampleDepartmentList());
        }

        [TearDown]
        public void Teardown()
        {
            DeleteDepartmentsFile();
        }

        #endregion

        private DepartmentDao _departmentDao;

        private static IList<Department> GetSampleDepartmentList()
        {
            return new List<Department>
                       {
                           new Department
                               {
                                   Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E3"),
                                   ContactName = "Test Person",
                                   ContactEmail = "b@a.no",
                                   SortOrder = 1
                               },
                           new Department
                               {
                                   Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E2"),
                                   ContactName = "Test 2",
                                   ContactEmail = "a@b.no",
                                   SortOrder = 2
                               },
                           new Department
                               {
                                   Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E1"),
                                   ContactName = "Test 3",
                                   ContactEmail = "c@c.no",
                                   SortOrder = 3
                               }
                       };
        }

        private static Department GetSampleDepartment(Guid id)
        {
            return new Department
                       {
                           Id = id,
                           ContactEmail = "z@z.com",
                           ContactName = "Test 4"
                       };
        }

        private void DeleteDepartmentsFile()
        {
            if (File.Exists(_departmentDao.DepartmentFileName))
            {
                File.Delete(_departmentDao.DepartmentFileName);
            }
        }


        [Test]
        public void Add_DepartmentIsNull_DoesntAdd()
        {
            _departmentDao.Add(null);

            Assert.That(_departmentDao.GetAll().Count, Is.EqualTo(3));
        }

        [Test]
        public void Add_DepartmentWithIdExists_UpdatesDepartment()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E3");
            Department department = GetSampleDepartment(id);

            _departmentDao.Add(department);

            Department departmentAfter = _departmentDao.Get(id);
            Assert.That(department, Is.Not.Null);
            Assert.That(departmentAfter.ContactEmail, Is.EqualTo(department.ContactEmail));
        }

        [Test]
        public void Add_NewDepartment_AddsDepartmentWithNewGuidAndMaxSortOrder()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000000");
            Department department = GetSampleDepartment(id);

            _departmentDao.Add(department);
            Assert.That(department.Id, Is.Not.EqualTo(new Guid("00000000-0000-0000-0000-000000000000")));
            Assert.That(department.SortOrder, Is.EqualTo(4));
        }


        [Test]
        public void Delete_ExistingDepartment_DeletesDepartment()
        {
            Department department = GetSampleDepartment(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"));

            _departmentDao.Delete(department);

            department = _departmentDao.Get(department.Id);
            Assert.That(department, Is.Null);
        }

        [Test]
        public void Delete_NoneExistingDepartment_DoesntChangeDepartmentList()
        {
            IList<Department> departmentsBeforeDelete = _departmentDao.GetAll();
            Department department = GetSampleDepartment(new Guid("55555555-CEB2-4faa-B6BF-329BF39FA1E4"));

            _departmentDao.Delete(department);

            IList<Department> departmentsAfterDelete = _departmentDao.GetAll();
            Assert.That(departmentsAfterDelete.Count, Is.EqualTo(departmentsBeforeDelete.Count));
        }

        [Test]
        public void GetAll_ReadExistingFile_ShouldReturnList()
        {
            _departmentDao.SaveAll(GetSampleDepartmentList());
            IList<Department> departments = _departmentDao.GetAll();
            Assert.That(departments.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetAll_ReadNonexistingFile_ShouldReturnEmptyList()
        {
            DeleteDepartmentsFile();
            IList<Department> departments = _departmentDao.GetAll();
            Assert.That(departments.Count, Is.EqualTo(0));
        }

        [Test]
        public void Get_IdDoesntExist_ReturnsNull()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-999999999999");
            Department department = _departmentDao.Get(id);
            Assert.That(department, Is.Null);
        }

        [Test]
        public void Get_IdExists_ReturnsDepartment()
        {
            var id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E1");
            Department department = _departmentDao.Get(id);
            Assert.That(department, Is.Not.Null);
        }

        [Test]
        public void SaveAll_DepartmentsIsEmpty_WritesFile()
        {
            _departmentDao.SaveAll(new List<Department>());
            IList<Department> departments = _departmentDao.GetAll();
            Assert.That(departments.Count, Is.EqualTo(0));
        }

        [Test]
        public void SaveAll_DepartmentsIsNull_WritesFile()
        {
            _departmentDao.SaveAll(null);
            IList<Department> departments = _departmentDao.GetAll();
            Assert.That(departments.Count, Is.EqualTo(0));
        }

        [Test]
        public void SaveAll_ListOfDepartments_SavesDepartments()
        {
            _departmentDao.SaveAll(GetSampleDepartmentList());

            IList<Department> departments = _departmentDao.GetAll();
            Assert.That(departments.Count, Is.EqualTo(3));
        }

        [Test]
        public void Update_DepartmentDoesntExist_ExpectedResult()
        {
            Department department = GetSampleDepartment(new Guid("F9168C5E-CEB2-4faa-B6BF-111111111111"));

            _departmentDao.Update(department);
            Department departmentSaved = _departmentDao.Get(new Guid("F9168C5E-CEB2-4faa-B6BF-111111111111"));

            Assert.That(departmentSaved, Is.Null);
        }

        [Test]
        public void Update_ExistingDepartment_UpdatesDepartment()
        {
            Department department = GetSampleDepartment(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E1"));

            _departmentDao.Update(department);
            Department departmentSaved = _departmentDao.Get(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E1"));

            Assert.That(departmentSaved.ContactEmail, Is.EqualTo(department.ContactEmail));
        }
    }
}