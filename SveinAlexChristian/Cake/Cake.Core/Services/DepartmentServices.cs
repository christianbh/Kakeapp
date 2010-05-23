using System.Collections.Generic;
using System.Linq;
using Cake.Core.Domain;

namespace Cake.Core.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        #region IDepartmentServices Members

        public IList<Department> RotatetDepartmentList(IList<Department> departments)
        {
            if (departments == null)
            {
                return new List<Department>();
            }

            if (departments.Count == 0)
            {
                return departments;
            }


            int lastSortOrder = departments.Max(d => d.SortOrder);

            Department department = FindDepartmentWithMinSortOrder(departments);

            if (department != null)
            {
                department.SortOrder = lastSortOrder + 1;
            }

            departments = (from d in departments orderby d.SortOrder select d).ToList();

            int sortOrder = 1;

            foreach (Department dep in departments)
            {
                dep.SortOrder = sortOrder;
                sortOrder++;
            }

            return departments;
        }

        public Department GetFirstDepartmentInList(IList<Department> departments)
        {
            if (departments == null || departments.Count == 0)
            {
                return new Department
                           {
                               ContactName = "No departments registred",
                               ContactEmail = string.Empty
                           };
            }

            return FindDepartmentWithMinSortOrder(departments);
        }

        #endregion

        private static Department FindDepartmentWithMinSortOrder(IList<Department> departments)
        {
            int firstSortOrder = departments.Min(d => d.SortOrder);

            return (from d in departments
                    where d.SortOrder == firstSortOrder
                    select d).First();
        }
    }
}