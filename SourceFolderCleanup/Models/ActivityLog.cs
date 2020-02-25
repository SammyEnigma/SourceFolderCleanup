using System;
using System.Collections.Generic;

namespace SourceFolderCleanup.Models
{
    public class ActivityLog
    {
        public DateTime Timestamp { get; set; }
        public Settings Settings { get; set; }
        public List<Folder> DeletedFolders { get; set; }
        public List<Folder> ArchivedFolders { get; set; }

        public class Folder
        {
            public string Path { get; set; }
            public long TotalSize { get; set; }
        }
    }
}
