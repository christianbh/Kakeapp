using Cake.Core.Domain;
using NUnit.Framework;

namespace Cake.UnitTests.Core.Domain
{
    [TestFixture]
    public class DepartmentTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _department = new Department();
        }

        #endregion

        private Department _department;


        [Test]
        public void Ctor_DefaultSettings_CreatesDepartmentObject()
        {
            Assert.That(_department, Is.Not.Null);
        }
    }
}