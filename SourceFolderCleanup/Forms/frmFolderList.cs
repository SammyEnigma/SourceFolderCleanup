using SourceFolderCleanup.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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
            foreach (var folder in Folders) list.Add(folder);

            BindingSource bs = new BindingSource();
            bs.DataSource = list;
            dgvFolders.DataSource = bs;
        }
    }    
}
