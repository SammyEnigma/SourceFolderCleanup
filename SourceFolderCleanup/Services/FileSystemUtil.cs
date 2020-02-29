using SourceFolderCleanup.Static;
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

        public async Task<long> GetFolderTotalSizeAsync(IEnumerable<FolderInfo> folders)
        {
            long result = 0;

            await Task.Run(() =>
            {
                foreach (var folder in folders)
                {
                    result += GetFolderSize(folder.Path);
                }
            });

            return result;
        }

        public long GetFolderSize(string path)
        {
            long result = 0;

            FileSystem.EnumFiles(path, "*", fileFound: (fi) =>
            {
                result += fi.Length;
                return EnumFileResult.Continue;
            });

            return result;
        }

        public async Task<IEnumerable<FolderInfo>> FilterFoldersOlderThanAsync(IEnumerable<FolderInfo> folders, int daysOld)
        {
            IEnumerable<FolderInfo> results = null;

            await Task.Run(() =>
            {
                results = FilterFoldersOlderThan(folders, daysOld);
            });

            return results;
        }

        public IEnumerable<FolderInfo> FilterFoldersOlderThan(IEnumerable<FolderInfo> folders, int daysOld)
        {
            DateTime cutOff = DateTime.Today.AddDays(daysOld * -1);
            return folders.Where(folder => GetFolderMaxDate(folder.Path) < cutOff);
        }

        public async Task<IEnumerable<FolderInfo>> GetBinObjFoldersAsync(string parentPath)
        {
            IEnumerable<FolderInfo> results = null;

            await Task.Run(() =>
            {
                results = GetBinObjFolders(parentPath);
            });

            return results;
        }

        public IEnumerable<FolderInfo> GetBinObjFolders(string parentPath)
        {
            return GetSubfoldersNamed(parentPath, new string[] { "bin", "obj" }, new string[] { "node_modules" });
        }

        public async Task<IEnumerable<FolderInfo>> GetPackagesFoldersAsync(string parentPath)
        {
            IEnumerable<FolderInfo> results = null;

            await Task.Run(() =>
            {
                results = GetPackagesFolders(parentPath);
            });

            return results;
        }

        public IEnumerable<FolderInfo> GetPackagesFolders(string parentPath)
        {
            return GetSubfoldersNamed(parentPath, new string[] { "packages" });
        }

        private IEnumerable<FolderInfo> GetSubfoldersNamed(string parentPath, string[] includeNames, string[] excludeNames = null)
        {
            List<FolderInfo> results = new List<FolderInfo>();

            FileSystem.EnumFiles(parentPath, "*", directoryFound: (di) =>
            {
                if (includeNames.Contains(di.Name) && (excludeNames?.All(name => !di.FullName.Contains(name)) ?? true))
                {
                    results.Add(new FolderInfo() 
                    { 
                        Path = di.FullName, 
                        TotalSize = GetFolderSize(di.FullName),
                        MaxDate = GetFolderMaxDate(di.FullName)
                    });
                    return EnumFileResult.NextFolder;
                }

                return EnumFileResult.Continue;
            });

            return results;
        }
    }

    public class FolderInfo
    {
        public string Path { get; set; }
        public long TotalSize { get; set; }
        public DateTime MaxDate { get; set; }
        public string SizeText { get { return Readable.FileSize(TotalSize); } }
        public bool IsSelected { get; set; }
    }
}
