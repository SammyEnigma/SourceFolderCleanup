using SourceFolderCleanup.Abstract;
using SourceFolderCleanup.Services;
using SourceFolderCleanup.Static;
using System;
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
        public long SelectedSize { get; private set; }

        private FolderInfoSorter _sorter;

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

                _sorter = new FolderInfoSorter(dgvFolders, new DataGridViewSorter<FolderInfo>.ColumnInfo() 
                {
                    Column = colSize,
                    IsAscending = false
                });
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
            UpdateSelectedSize();
        }

        private void UpdateSelectedSize()
        {
            var dataSource = dgvFolders.DataSource as BindingSource;
            var list = dataSource.DataSource as BindingList<FolderInfo>;
            SelectedSize = list.Where(item => item.IsSelected).Sum(item => item.TotalSize);
            lblSelectedSize.Text = Readable.FileSize(SelectedSize);
        }

        private void frmFolderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Position = FormPosition.FromForm(this);
        }

        private void llSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow row in dgvFolders.Rows)
            {
                row.Cells["colSelected"].Value = true;
            }

            UpdateSelectedSize();
        }

        private void llInvertSelection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow row in dgvFolders.Rows)
            {
                bool value = (bool)row.Cells["colSelected"].Value;
                row.Cells["colSelected"].Value = !value;
            }

            UpdateSelectedSize();
        }
    }        
}
