using System;
using System.IO;
using Cake.DAL;
using NUnit.Framework;

namespace Cake.IntegrationTests.DAL
{
    [TestFixture]
    public class RepositoryWriterTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            DeleteNonsensFolder();
            _repositoryWriter = new RepositoryWriter();
        }

        #endregion

        private const string FilePath = @"c:\nonsens\test.cake";
        private const string DirectoryPath = @"c:\nonsens\";
        private RepositoryWriter _repositoryWriter;

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
            CreateNonsensFolder();
            _repositoryWriter.Write(FilePath, "nonsens");
            bool fileExists = File.Exists(FilePath);

            Assert.That(fileExists, Is.EqualTo(true));
        }

        [Test]
        public void Write_PathDoesNotExist_ThrowsDirectoryNotFoundException()
        {
            Assert.Throws<DirectoryNotFoundException>(() => _repositoryWriter.Write(FilePath, "nonsens"));
        }

        [Test]
        public void Write_ValueToSaveIsNull_ThrowsArgumentNullException()
        {
            CreateNonsensFolder();
            Assert.Throws<ArgumentNullException>(() => _repositoryWriter.Write(FilePath, null));
        }
    }
}