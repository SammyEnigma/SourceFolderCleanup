using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceFolderCleanup.Services;

namespace SourceFolderCleanup.Tests
{
    [TestClass]
    public class FileSystemUtilTests
    {
        [TestMethod]
        public void GetBinFolders()
        {
            var fsu = new FileSystemUtil();
            var results = fsu.GetBinObjFoldersAsync(@"C:\Users\Adam\Source\Repos").Result;
            Assert.IsTrue(results.All(folder => folder.Path.EndsWith("bin") || folder.Path.EndsWith("obj")));
            Assert.IsTrue(results.Any(folder => folder.Path.EndsWith("obj")));
        }

        [TestMethod]
        public void GetPackagesFolders()
        {
            var fsu = new FileSystemUtil();
            var results = fsu.GetPackagesFoldersAsync(@"C:\Users\Adam\Source\Repos").Result;
            Assert.IsTrue(results.All(folder => folder.Path.EndsWith("packages")));
        }

        [TestMethod]
        public void GetBinFoldersOlderThan()
        {
            var fsu = new FileSystemUtil();
            var results = fsu.GetBinObjFoldersAsync(@"C:\Users\Adam\Source\Repos").Result;
            var cutoffDate = DateTime.Today.AddDays(-90);
            var deleteable = results.Where(folder => fsu.GetFolderMaxDate(folder.Path) < cutoffDate).ToArray();
        }
    }
}
