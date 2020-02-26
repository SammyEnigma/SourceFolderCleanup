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
            var results = new FileSystemUtil().GetSubfoldersNamed(@"C:\Users\Adam\Source\Repos", new string[] { "bin", "obj" }, new string[] { "node_modules" });
            Assert.IsTrue(results.All(name => name.EndsWith("bin") || name.EndsWith("obj")));
            Assert.IsTrue(results.Any(name => name.EndsWith("obj")));
        }

        [TestMethod]
        public void GetBinFoldersOlderThan()
        {
            var fsu = new FileSystemUtil();
            var results = fsu.GetBinObjFolders(@"C:\Users\Adam\Source\Repos");
            var cutoffDate = DateTime.Today.AddDays(-90);
            var deleteable = results.Where(path => fsu.GetFolderMaxDate(path) < cutoffDate).ToArray();
        }
    }
}
