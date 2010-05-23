using System.IO;
using Cake.DAL;
using NUnit.Framework;

namespace Cake.IntegrationTests.DAL
{
    [TestFixture]
    public class RepositoryReaderTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            DeleteNonsensFolder();
            _repositoryReader = new RepositoryReader();
        }

        #endregion

        private const string FilePath = @"c:\nonsens\test.cake";
        private const string DirectoryPath = @"c:\nonsens\";
        private RepositoryReader _repositoryReader;

        private void WriteNonsensFile()
        {
            CreateNonsensFolder();
            var repositoryWriter = new RepositoryWriter();
            repositoryWriter.Write(FilePath, "string");
        }

        private void CreateNonsensFolder()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
        }

        private void DeleteNonsensFolder()
        {
            if (Directory.Exists(DirectoryPath))
            {
                string[] filePaths = Directory.GetFiles(DirectoryPath);
                DeleteFiles(filePaths);
                Directory.Delete(DirectoryPath);
            }
        }

        private void DeleteFiles(string[] filePaths)
        {
            foreach (string path in filePaths)
            {
                File.Delete(path);
            }
        }

        [Test]
        public void Write_PathDoesExist_CreatesFile()
        {
            WriteNonsensFile();
            _repositoryReader.Read(FilePath);
            bool fileExists = File.Exists(FilePath);

            Assert.That(fileExists, Is.EqualTo(true));
        }

        [Test]
        public void Write_PathDoesNotExist_ThrowsDirectoryNotFoundException()
        {
            Assert.Throws<DirectoryNotFoundException>(() => _repositoryReader.Read(FilePath));
        }
    }
}