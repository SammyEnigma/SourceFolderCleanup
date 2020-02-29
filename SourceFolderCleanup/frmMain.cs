using JsonSettings.Library;
using SourceFolderCleanup.Controls;
using SourceFolderCleanup.Forms;
using SourceFolderCleanup.Models;
using SourceFolderCleanup.Services;
using SourceFolderCleanup.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Extensions.ComboBoxes;
using WinForms.Library.Models;

namespace SourceFolderCleanup
{
    public partial class frmMain : Form
    {
        private Settings _settings;
        private ControlBinder<Settings> _binder;

        private IEnumerable<FolderInfo> _deleteableBinObj;
        private IEnumerable<FolderInfo> _deleteablePackages;

        public frmMain()
        {
            InitializeComponent();
            pllBinObjSize.LinkClicked += delegate(object sender, LinkLabelLinkClickedEventArgs e) { ShowFolderList(_deleteableBinObj); };
            pllPackagesSize.LinkClicked += delegate (object sender, LinkLabelLinkClickedEventArgs e) { ShowFolderList(_deleteablePackages); };
        }

        private void ShowFolderList(IEnumerable<FolderInfo> folders)
        {
            var dlg = new frmFolderList() { Position = _settings.FolderListPosition };
            dlg.Folders = folders;
            dlg.ShowDialog();
            _settings.FolderListPosition = dlg.Position;
        }

        private async void frmMain_Load(object sender, System.EventArgs e)
        {
            var monthValues = new int[] { 3, 6, 9, 12 }.Select(i => new ListItem<int>(i, i.ToString()));

            _settings = SettingsBase.Load<Settings>();
            _binder = new ControlBinder<Settings>();            

            _binder.Add(tbSourcePath, model => model.SourcePath);
            _binder.Add(chkDelete, model => model.Delete);
            _binder.Add(chkDeleteBinObj, model => model.DeleteBinAndObj);
            _binder.Add(chkDeletePackages, model => model.DeletePackages);
            
            _binder.AddItems(cbDeleteMonths,
                (model) => model.DeleteMonthsOld = cbDeleteMonths.GetValue<int>(),
                (model) => cbDeleteMonths.SetValue(model.DeleteMonthsOld), monthValues);

            _binder.Add(chkArchive, model => model.Archive);
            _binder.Add(tbArchivePath, model => model.ArchivePath);
            _binder.AddItems(cbArchiveMonths,
                (model) => model.ArchiveMonthsOld = cbArchiveMonths.GetValue<int>(),
                (model) => cbArchiveMonths.SetValue(model.ArchiveMonthsOld), monthValues);

            _binder.Document = _settings;
            chkArchive_CheckedChanged(null, new EventArgs());
            chkDelete_CheckedChanged(null, new EventArgs());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.Save();
        }

        private void tbArchivePath_BuilderClicked(object sender, WinForms.Library.Controls.BuilderEventArgs e)
        {
            tbArchivePath.SelectFolder(e);
        }

        private void tbSourcePath_BuilderClicked(object sender, WinForms.Library.Controls.BuilderEventArgs e)
        {
            tbSourcePath.SelectFolder(e);
        }

        private void chkArchive_CheckedChanged(object sender, EventArgs e)
        {
            cbArchiveMonths.Enabled = chkArchive.Checked;
            tbArchivePath.Enabled = chkArchive.Checked;
            linkLabel4.Enabled = chkArchive.Checked;
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            chkDeleteBinObj.Enabled = chkDelete.Checked;
            chkDeletePackages.Enabled = chkDelete.Checked;
            cbDeleteMonths.Enabled = chkDelete.Checked;
            pllBinObjSize.Enabled = chkDelete.Checked;
            pllPackagesSize.Enabled = chkDelete.Checked;
        }

        private async void cbDeleteMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            pllBinObjSize.Visible = false;
            pllPackagesSize.Visible = false;
            pllBinObjSize.Mode = ProgressLinkLabelMode.Progress;
            pllPackagesSize.Mode = ProgressLinkLabelMode.Progress;

            List<Task> tasks = new List<Task>();
            if (_settings.DeleteBinAndObj)
            {
                pllBinObjSize.Visible = true;
                tasks.Add(AnalyzeFolderAsync(pllBinObjSize, _settings.DeleteMonthsOld, 
                    async (fsu) => await fsu.GetBinObjFoldersAsync(_settings.SourcePath),
                    (results) => _deleteableBinObj = results));
            }

            if (_settings.DeletePackages)
            {
                pllPackagesSize.Visible = true;
                tasks.Add(AnalyzeFolderAsync(pllPackagesSize, _settings.DeleteMonthsOld, 
                    async (fsu) => await fsu.GetPackagesFoldersAsync(_settings.SourcePath),
                    (results) => _deleteablePackages = results));
            }

            await Task.WhenAll(tasks);

            pllBinObjSize.Enabled = chkDelete.Checked;
            pllPackagesSize.Enabled = chkDelete.Checked;
        }

        private async Task AnalyzeFolderAsync(
            ProgressLinkLabel linkLabel, int monthsOld, 
            Func<FileSystemUtil, Task<IEnumerable<FolderInfo>>> getFolders, 
            Action<IEnumerable<FolderInfo>> captureResults)
        {
            var fsu = new FileSystemUtil();

            var folders = await getFolders.Invoke(fsu);
            var deletable = folders.Where(folder => folder.IsMonthsOld(monthsOld));
            captureResults.Invoke(deletable);
            long deleteableBytes = await fsu.GetFolderTotalSizeAsync(deletable);
            linkLabel.Mode = ProgressLinkLabelMode.Text;
            linkLabel.Text = Readable.FileSize(deleteableBytes);
        }
    }
}
