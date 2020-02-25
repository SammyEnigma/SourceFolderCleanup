using System;
using System.Collections.Generic;

namespace SourceFolderCleanup.Models
{
    public class ActivityLog
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Settings Settings { get; set; }
        public List<Folder> Deleted { get; set; }
        public List<Archive> Archived { get; set; }

        public class Folder
        {
            public string Path { get; set; }
            public long TotalSize { get; set; }
        }

        public class Archive
        {
            public string SourcePath { get; set; }
            public string OutputZipFile { get; set; }
        }
    }
}
