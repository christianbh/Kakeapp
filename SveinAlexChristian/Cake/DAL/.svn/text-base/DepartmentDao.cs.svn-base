using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;

namespace Cake.DAL
{
    public class DepartmentDao : IDepartmentDao
    {
        private readonly IRepositoryReader _repositoryReader;
        private readonly IRepositoryWriter _repositoryWriter;
        private string _repositoryPath;

        public DepartmentDao(string repositoryPath)
        {
            _repositoryWriter = new RepositoryWriter();
            _repositoryReader = new RepositoryReader();
            _repositoryPath = repositoryPath;
        }

        public DepartmentDao(IRepositoryWriter repositoryWriter)
        {
            _repositoryWriter = repositoryWriter;
            _repositoryReader = new RepositoryReader();
        }

        public DepartmentDao(IRepositoryReader repositoryReader)
        {
            _repositoryReader = repositoryReader;
            _repositoryWriter = new RepositoryWriter();
        }

        public DepartmentDao(IRepositoryReader reposetoryReader, IRepositoryWriter reposetoryWriter)
        {
            _repositoryReader = reposetoryReader;
            _repositoryWriter = reposetoryWriter;
        }

        public string DepartmentFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_repositoryPath))
                    _repositoryPath = Path.GetTempPath();

                return _repositoryPath + "\\Departments.cake";
            }
        }

        #region IDepartmentDao Members

        public IList<Department> GetAll()
        {
            if (!File.Exists(DepartmentFileName))
                return new List<Department>();

            var departments = (IList<Department>) _repositoryReader.Read(DepartmentFileName);

            return (from d in departments orderby d.SortOrder select d).ToList();
        }

        public void SaveAll(IList<Department> departments)
        {
            if (departments == null)
                departments = new List<Department>();

            _repositoryWriter.Write(DepartmentFileName, departments);
        }

        public Department Get(Guid id)
        {
            IList<Department> departments = GetAll();

            return (from d in departments
                    where d.Id == id
                    select d).FirstOrDefault();
        }

        public void Add(Department department)
        {
            if (department == null) return;

            if (department.Id.Equals(new Guid("00000000-0000-0000-0000-000000000000")))
                department.Id = Guid.NewGuid();

            if (DepartmentExists(department))
            {
                Update(department);
                return;
            }

            IList<Department> departments = GetAll();

            department.SortOrder = departments.Count + 1;

            departments.Add(department);
            SaveAll(departments);
        }

        public void Delete(Department department)
        {
            IList<Department> departments = GetAll();

            List<Department> departmentsToKeep = (from d in departments
                                                  where d.Id != department.Id
                                                  select d).ToList();

            SaveAll(departmentsToKeep);
        }

        public void Update(Department department)
        {
            IList<Department> departments = GetAll();

            Department departmentToUpdate = (from d in departments
                                             where d.Id == department.Id
                                             select d).FirstOrDefault();

            if (departmentToUpdate == null)
                return;

            departments.Remove(departmentToUpdate);
            departments.Add(department);
            SaveAll(departments);
        }

        #endregion

        private bool DepartmentExists(Department department)
        {
            return Get(department.Id) != null;
        }
    }
}