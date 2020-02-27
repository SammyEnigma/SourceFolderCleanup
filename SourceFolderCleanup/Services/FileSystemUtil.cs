using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WinForms.Library;

namespace SourceFolderCleanup.Services
{
    public class FileSystemUtil
    {
        /// <summary>
        /// Finds the maximum date modified of files in a path
        /// </summary>
        public DateTime GetFolderMaxDate(string path)
        {
            DateTime result = DateTime.MinValue;

            FileSystem.EnumFiles(path, "*", fileFound: (fi) =>
            {
                if (fi.LastWriteTimeUtc > result)
                {
                    result = fi.LastWriteTimeUtc;
                }

                return EnumFileResult.Continue;
            });
            
            return result;
        }               

        public IEnumerable<string> FilterFoldersOlderThan(IEnumerable<string> folders, int daysOld)
        {
            DateTime cutOff = DateTime.Today.AddDays(daysOld * -1);
            return folders.Where(path => GetFolderMaxDate(path) < cutOff);
        }

        public async Task<IEnumerable<string>> GetBinObjFoldersAsync(string parentPath)
        {
            IEnumerable<string> results = null;

            await Task.Run(() =>
            {
                results = GetBinObjFolders(parentPath);
            });

            return results;
        }

        public IEnumerable<string> GetBinObjFolders(string parentPath)
        {
            return GetSubfoldersNamed(parentPath, new string[] { "bin", "obj" }, new string[] { "node_modules" });
        }

        public async Task<IEnumerable<string>> GetPackagesFoldersAsync(string parentPath)
        {
            IEnumerable<string> results = null;

            await Task.Run(() =>
            {
                results = GetPackagesFolders(parentPath);
            });

            return results;
        }

        public IEnumerable<string> GetPackagesFolders(string parentPath)
        {
            return GetSubfoldersNamed(parentPath, new string[] { "packages" });
        }

        private IEnumerable<string> GetSubfoldersNamed(string parentPath, string[] includeNames, string[] excludeNames = null)
        {
            List<string> results = new List<string>();

            FileSystem.EnumFiles(parentPath, "*", directoryFound: (di) =>
            {
                if (includeNames.Contains(di.Name) && (excludeNames?.All(name => !di.FullName.Contains(name)) ?? true))
                {
                    results.Add(di.FullName);
                    return EnumFileResult.NextFolder;
                }

                return EnumFileResult.Continue;
            });

            return results;
        }
    }
}
