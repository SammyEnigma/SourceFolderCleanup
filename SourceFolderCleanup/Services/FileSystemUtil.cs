using System;
using System.Collections.Generic;
using System.IO;
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

        public IEnumerable<string> GetSubfoldersNamed(string parentPath, string folderName)
        {
            List<string> results = new List<string>();

            FileSystem.EnumFiles(parentPath, "*", directoryFound: (di) =>
            {
                if (di.Name.Equals(folderName))
                {
                    results.Add(di.FullName);
                    return EnumFileResult.Stop;
                }

                return EnumFileResult.Continue;
            });

            return results;
        }
    }
}
