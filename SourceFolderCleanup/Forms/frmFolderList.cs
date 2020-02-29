using SourceFolderCleanup.Services;
using SourceFolderCleanup.Static;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Models;

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
        public FormPosition Position { get; set; }

        private void frmFolderList_Load(object sender, System.EventArgs e)
        {
            if (Position != null) Position.Apply(this);

            try
            {
                SuspendLayout();
                var list = new BindingList<FolderInfo>();
                foreach (var folder in Folders.OrderByDescending(item => item.TotalSize)) list.Add(folder);

                BindingSource bs = new BindingSource();
                bs.DataSource = list;
                dgvFolders.DataSource = bs;

                lblTotalSize.Text = Readable.FileSize(Folders.Sum(item => item.TotalSize));
            }
            finally
            {
                ResumeLayout();
            }            
        }

        private void dgvFolders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colPath.Index && e.RowIndex > -1)
            {
                var item = dgvFolders.Rows[e.RowIndex].DataBoundItem as FolderInfo;
                FileSystem.RevealInExplorer(item.Path);
            }
        }

        private void dgvFolders_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dataSource = dgvFolders.DataSource as BindingSource;
            var list = dataSource.DataSource as BindingList<FolderInfo>;
            long selectedSize = list.Where(item => item.IsSelected).Sum(item => item.TotalSize);
            lblSelectedSize.Text = Readable.FileSize(selectedSize);
        }

        private void frmFolderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Position = FormPosition.FromForm(this);
        }
    }    
}
