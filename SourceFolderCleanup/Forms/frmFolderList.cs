using SourceFolderCleanup.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library;

namespace SourceFolderCleanup.Forms
{
    public partial class frmFolderList : Form
    {
        public frmFolderList()
        {
            InitializeComponent();
            dgvFolders.AutoGenerateColumns = false;
        }

        public IEnumerable<FolderInfo> Folders { get; set; }

        private void frmFolderList_Load(object sender, System.EventArgs e)
        {
            var list = new BindingList<FolderInfo>();
            foreach (var folder in Folders.OrderByDescending(item => item.TotalSize)) list.Add(folder);

            BindingSource bs = new BindingSource();
            bs.DataSource = list;
            dgvFolders.DataSource = bs;
        }

        private void dgvFolders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colPath.Index && e.RowIndex > -1)
            {
                var item = dgvFolders.Rows[e.RowIndex].DataBoundItem as FolderInfo;
                FileSystem.RevealInExplorer(item.Path);
            }
        }
    }    
}
