using System.IO;
using FileManagement;
using NUnit.Framework;

namespace AskDannyTests.Integration
{
    [TestFixture]
    public class FileManagerTests
    {
        private const string FileStoragePath = @"C:\JsonFile.txt";

        [Test]
        public void FileManager_ShouldSaveJsonFile()
        {
            var testString = "My Test String";
            var fileMan = new FileManager();
            fileMan.SaveToFile(testString);

            Assert.That(File.Exists(FileStoragePath));
        }

        [Test]
        public void FileManager_ShouldReadJsonFile()
        {
            var testString = "My Test String";
            var fileMan = new FileManager();

            fileMan.SaveToFile(testString);
            var file = fileMan.LoadFile();

            Assert.That(File.Exists(FileStoragePath));
            Assert.That(file.Contains(testString));
        }
    }
}