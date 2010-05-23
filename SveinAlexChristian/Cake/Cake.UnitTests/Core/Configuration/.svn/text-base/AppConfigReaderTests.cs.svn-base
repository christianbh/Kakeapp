using System;
using System.Configuration;
using System.Reflection;
using Cake.Core.Configuration;
using NUnit.Framework;

namespace Cake.UnitTests.Core.Configuration
{
    [TestFixture]
    public class AppConfigReaderTests
    {
        [Test]
        public void GetAppConfigValue_ShouldReturnValue_ForAllAppSettingsInAppConfig()
        {
            Type type = typeof (AppConfigReader);
            var appConfigReader = new AppConfigReader();

            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                PropertyInfo propertyInfo = type.GetProperty(key);
                object propertyValue = propertyInfo.GetValue(appConfigReader, null);

                if (propertyValue.GetType().Equals(typeof (string)))
                    Assert.IsTrue(((string) propertyValue).Length > 0);
                else if (propertyValue.GetType().Equals(typeof (bool)))
                    Assert.IsTrue(((bool) propertyValue).Equals(true) || ((bool) propertyValue).Equals(false));
                else
                    Assert.IsTrue(((int) propertyValue) > 0);
            }
        }
    }
}