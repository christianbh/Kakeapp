using System.IO;
using Cake.Core.Logging;
using NUnit.Framework;

namespace Cake.IntegrationTests.Core.Logging
{
    [TestFixture]
    public class ServiceLoggerTests
    {
        private ServiceLogger _serviceLogger;
        private const string _LOGFILEPATH = @"c:\temp\cakeScheduleService.log";

        [Test]
        public void Write_FileDoesNotExist_ShouldCreateFile()
        {
            File.Delete(_LOGFILEPATH);
            _serviceLogger = new ServiceLogger(_LOGFILEPATH);

            _serviceLogger.Write("cake schedule service");

            Assert.That(File.Exists(_LOGFILEPATH), Is.EqualTo(true));
        }

        [Test]
        public void Write_FolderInLogFilePathDoesNotExist_ShouldNotThrowException()
        {
            _serviceLogger = new ServiceLogger(@"c:\NotExistingFolder\CakeScheduleService.log");

            Assert.DoesNotThrow(() => _serviceLogger.Write("Some message"));
        }
    }
}