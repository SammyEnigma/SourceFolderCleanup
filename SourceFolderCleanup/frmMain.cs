﻿using JsonSettings.Library;
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

        public frmMain()
        {
            InitializeComponent();
            pllBinObjSize.LinkClicked += delegate(object sender, LinkLabelLinkClickedEventArgs e) { ShowFolderList(sender as LinkLabel, _settings.BinObjFolders); };
            pllPackagesSize.LinkClicked += delegate (object sender, LinkLabelLinkClickedEventArgs e) { ShowFolderList(sender as LinkLabel, _settings.PackagesFolders); };
        }

        private void ShowFolderList(LinkLabel control, IEnumerable<FolderInfo> folders)
        {
            var dlg = new frmFolderList() { Position = _settings.FolderListPosition };
            dlg.Folders = folders;
            dlg.ShowDialog();
            _settings.FolderListPosition = dlg.Position;
            
            control.Text = (dlg.SelectedSize != 0) ?
                $"{Readable.FileSize(folders.Sum(item => item.TotalSize))}, {Readable.FileSize(dlg.SelectedSize)} selected" :
                Readable.FileSize(folders.Sum(item => item.TotalSize));

            UpdateDeleteButtonText();
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
            UpdateArchiveInfo();
            UpdateDeleteInfo();
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
            UpdateArchiveInfo();
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDeleteInfo();
        }

        private void UpdateArchiveInfo()
        {
            cbArchiveMonths.Enabled = chkArchive.Checked;
            tbArchivePath.Enabled = chkArchive.Checked;
            linkLabel4.Enabled = chkArchive.Checked;
        }

        private void UpdateDeleteInfo()
        {
            chkDeleteBinObj.Enabled = chkDelete.Checked;
            chkDeletePackages.Enabled = chkDelete.Checked;
            cbDeleteMonths.Enabled = chkDelete.Checked;
            pllBinObjSize.Enabled = chkDelete.Checked;
            pllPackagesSize.Enabled = chkDelete.Checked;
        }

        private async void cbDeleteMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            pllBinObjSize.Visible = false;
            pllPackagesSize.Visible = false;
            pllBinObjSize.Mode = ProgressLinkLabelMode.Progress;
            pllPackagesSize.Mode = ProgressLinkLabelMode.Progress;

            // data binding hasn't happened yet, so we need to read the combo box directly
            int monthsOld = cbDeleteMonths.GetValue<int>();

            await AnalyzeFolders(monthsOld);
        }

        private async Task AnalyzeFolders(int monthsOld)
        {
            List<Task> tasks = new List<Task>();
            if (_settings.DeleteBinAndObj)
            {
                pllBinObjSize.Visible = true;
                tasks.Add(AnalyzeBinObj(monthsOld));
            }

            if (_settings.DeletePackages)
            {
                pllPackagesSize.Visible = true;
                tasks.Add(AnalyzePackages(monthsOld));
            }

            await Task.WhenAll(tasks);

            pllBinObjSize.Enabled = chkDelete.Checked;
            pllPackagesSize.Enabled = chkDelete.Checked;

            btnDelete.Enabled = true;
            UpdateDeleteButtonText();
        }

        private Task AnalyzePackages(int monthsOld)
        {
            return AnalyzeFolderAsync(
                pllPackagesSize, monthsOld,
                async (fsu) => await fsu.GetPackagesFoldersAsync(_settings.SourcePath),
                (results) => _settings.PackagesFolders = results.ToList());
        }

        private Task AnalyzeBinObj(int monthsOld)
        {
            return AnalyzeFolderAsync(
                pllBinObjSize, monthsOld,
                async (fsu) => await fsu.GetBinObjFoldersAsync(_settings.SourcePath),
                (results) => _settings.BinObjFolders = results.ToList());
        }

        private void UpdateDeleteButtonText()
        {
            long deleteSize = GetDeletableFolders().Sum(item => item.TotalSize);
            btnDelete.Text = $"Delete {Readable.FileSize(deleteSize)}";
            btnDelete.Enabled = (deleteSize > 0);
        }

        private IEnumerable<FolderInfo> GetDeletableFolders()
        {
            List<FolderInfo> results = new List<FolderInfo>();

            if (chkDeleteBinObj.Checked && _settings.BinObjFolders != null)
            {
                results.AddRange((_settings.BinObjFolders.Any(item => item.IsSelected)) ?
                    _settings.BinObjFolders.Where(item => item.IsSelected) :
                    _settings.BinObjFolders);
            }

            if (chkDeletePackages.Checked && _settings.PackagesFolders != null)
            {
                results.AddRange((_settings.PackagesFolders.Any(item => item.IsSelected)) ?
                    _settings.PackagesFolders.Where(item => item.IsSelected) :
                    _settings.PackagesFolders);
            }

            return results;
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
            long deleteableBytes = deletable.Sum(item => item.TotalSize);
            linkLabel.Mode = ProgressLinkLabelMode.Text;
            linkLabel.Text = Readable.FileSize(deleteableBytes);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var folders = GetDeletableFolders().Select(fi => fi.Path);

                IProgress<string> progress = new Progress<string>(ShowCurrentDeletingFolder);
                await Task.Run(() =>
                {
                    foreach (var folder in folders)
                    {
                        // thanks to https://stackoverflow.com/a/27522561/2023653 although still a little confusing
                        progress.Report(folder);
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(folder,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently,
                            Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                    }
                });

                int monthsOld = cbDeleteMonths.GetValue<int>();
                await AnalyzeFolders(monthsOld);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                lblDeleting.Visible = false;
                llCancelDelete.Visible = false;
            }            
        }

        private void ShowCurrentDeletingFolder(string path)
        {
            lblDeleting.Visible = true;
            llCancelDelete.Visible = true;
            lblDeleting.Text = path;
        }

        private async void chkDeleteBinObj_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeleteBinObj.Checked)
            {
                pllBinObjSize.Visible = true;
                int monthsOld = cbDeleteMonths.GetValue<int>();
                await AnalyzeBinObj(monthsOld);
            }
            else
            {
                pllBinObjSize.Visible = false;
            }

            UpdateDeleteButtonText();
        }

        private async void chkDeletePackages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletePackages.Checked)
            {
                pllPackagesSize.Visible = true;
                int monthsOld = cbDeleteMonths.GetValue<int>();
                await AnalyzePackages(monthsOld);
            }
            else
            {
                pllPackagesSize.Visible = false;
            }

            UpdateDeleteButtonText();
        }
    }
}
