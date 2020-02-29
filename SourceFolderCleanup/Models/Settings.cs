using JsonSettings.Library;
using SourceFolderCleanup.Services;
using System.Collections.Generic;
using WinForms.Library.Models;
using static System.Environment;

namespace SourceFolderCleanup.Models
{
    public class Settings : SettingsBase
    {
        public string SourcePath { get; set; }

        public bool Delete { get; set; }
        public int DeleteMonthsOld { get; set; }
        public bool DeleteBinAndObj { get; set; }
        public bool DeletePackages { get; set; }
        
        public bool Archive { get; set; }
        public int ArchiveMonthsOld { get; set; }
        public string ArchivePath { get; set; }

        public FormPosition FolderListPosition { get; set; }
        public List<FolderInfo> BinObjFolders { get; set; }
        public List<FolderInfo> PackagesFolders { get; set; }

        public override string Filename => BuildPath(SpecialFolder.LocalApplicationData, "SourceFolderCleanup", "settings.json");
    }
}
