namespace SourceFolderCleanup.Models
{
    public class Settings
    {
        public string[] SourcePaths { get; set; }
        public bool DeleteBinAndObj { get; set; }
        public bool DeletePackages { get; set; }
        public int DeleteMonthsOld { get; set; }
        public int ArchiveMonthsOld { get; set; }
        public string ArchiveLocation { get; set; }
    }
}
