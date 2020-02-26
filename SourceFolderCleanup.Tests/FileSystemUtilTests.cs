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
            var results = new FileSystemUtil().GetSubfoldersNamed(@"C:\Users\Adam\Source\Repos", "bin");
            Assert.IsTrue(results.All(name => name.EndsWith("bin")));
        }

        [TestMethod]
        public void GetBinFoldersOlderThan()
        {
            var fsu = new FileSystemUtil();
            var results = fsu.GetSubfoldersNamed(@"C:\Users\Adam\Source\Repos", "bin");
            var cutoffDate = DateTime.Today.AddDays(-90);
            var archiveable = results.Where(path => fsu.GetFolderMaxDate(path) < cutoffDate).ToArray();
        }
    }
}
