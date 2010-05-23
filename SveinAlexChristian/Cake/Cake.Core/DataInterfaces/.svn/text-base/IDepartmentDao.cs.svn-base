using System;
using System.Collections.Generic;
using Cake.Core.Domain;

namespace Cake.Core.DataInterfaces
{
    public interface IDepartmentDao
    {
        IList<Department> GetAll();
        void SaveAll(IList<Department> departments);
        Department Get(Guid id);
        void Add(Department department);
        void Delete(Department department);
        void Update(Department department);
    }
}