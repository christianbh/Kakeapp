using System.Collections.Generic;
using System.Linq;
using Cake.Core.Domain;
using Cake.Core.Services;
using NUnit.Framework;

namespace Cake.UnitTests.Core.Services
{
    [TestFixture]
    public class DepartmentServicesTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _departmentServices = new DepartmentServices();
        }

        #endregion

        private DepartmentServices _departmentServices;

        private static IList<Department> GetSampleDepartments()
        {
            IList<Department> departments = new List<Department>();

            for (int i = 1; i < 5; i++)
            {
                var dep = new Department
                              {
                                  ContactName = "dep" + i,
                                  ContactEmail = "dep" + i + "@firm.no",
                                  SortOrder = i
                              };
                departments.Add(dep);
            }

            return departments;
        }

        [Test]
        public void GetFirstDepartmentInList_ListEmpty_ShouldReturnDepartmentWithEmptyContactEmail()
        {
            Department department = _departmentServices.GetFirstDepartmentInList(new List<Department>());
            Assert.That(department.ContactEmail, Is.Empty);
        }

        [Test]
        public void GetFirstDepartmentInList_ListIsNull_ShouldReturnDepartment()
        {
            Department department = _departmentServices.GetFirstDepartmentInList(null);
            Assert.That(department, Is.Not.Null);
        }

        [Test]
        public void GetFirstDepartmentInList_ListWithDepartments_ShouldReturnFirstDepartmentInList()
        {
            IList<Department> departments = GetSampleDepartments();
            Department department = _departmentServices.GetFirstDepartmentInList(departments);
            Assert.That(department.SortOrder, Is.EqualTo(1));
        }

        [Test]
        public void RotatetDepartmentList_ListIsEmpty_ReturnEmptyList()
        {
            IList<Department> departments = _departmentServices.RotatetDepartmentList(new List<Department>());
            Assert.That(departments.Count, Is.EqualTo(0));
        }

        [Test]
        public void RotatetDepartmentList_ListIsNull_ListIsNotNull()
        {
            IList<Department> departments = _departmentServices.RotatetDepartmentList(null);
            Assert.That(departments, Is.Not.Null);
        }

        [Test]
        public void RotatetDepartmentList_ListOfDepartmentsWithHole_ReturnedListShouldNotHaveHole()
        {
            IList<Department> departments = GetSampleDepartments();

            Department depToRemove = (from d in departments
                                      where d.SortOrder == 2
                                      select d).First();
            departments.Remove(depToRemove);

            departments = _departmentServices.RotatetDepartmentList(departments);
            Department depNoFour = (from d in departments
                                    where d.SortOrder == 4
                                    select d).FirstOrDefault();

            Assert.That(depNoFour, Is.EqualTo(null));
        }

        [Test]
        public void RotatetDepartmentList_ListOfDepartments_FirsShouldBeLastAndSecondShouldBeFirst()
        {
            IList<Department> departments = GetSampleDepartments();
            departments = _departmentServices.RotatetDepartmentList(departments);

            Department depWithSortOrderOne = (from d in departments
                                              where d.SortOrder == 1
                                              select d).First();

            Department depUsedToBeFirst = (from d in departments
                                           where d.ContactEmail == "dep1@firm.no"
                                           select d).First();

            Assert.That(depWithSortOrderOne.ContactEmail, Is.EqualTo("dep2@firm.no"));
            Assert.That(depUsedToBeFirst.SortOrder, Is.EqualTo(4));
        }
    }
}