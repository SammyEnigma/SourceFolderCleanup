using JsonSettings.Library;
using SourceFolderCleanup.Models;
using SourceFolderCleanup.Services;
using System;
using System.Linq;
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
            linkLabel2.Enabled = chkDelete.Checked;
            linkLabel3.Enabled = chkDelete.Checked;
        }

        private async void cbDeleteMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fsu = new FileSystemUtil();
            
            if (_settings.DeleteBinAndObj)
            {
                
            }

            if (_settings.DeletePackages)
            {

            }
        }

        //private async Task
    }
}
